using TestAInspector.Agent.Contracts.Enums;
using TestAInspector.Agent.Contracts.Interfaces;
using TestAInspector.Analysis.Contracts.Enums;
using TestAInspector.Analysis.Contracts.Models;
using TestAInspector.Entities.Models;
using TestAInspector.Parsing.Contracts.Enums;
using TestAInspector.Parsing.Contracts.Interfaces;
using TestAInspector.Services.Contracts.Interfaces;
using TestAInspector.Services.Contracts.Models;
using TestAInspector.Validation.Contracts.Interfaces;
using TestAInspector.Validation.Contracts.Models.Batch.Enums;

namespace TestAInspector.Services;

/// <summary>
/// <inheritdoc cref="IPipelineService"/>
/// </summary>
public class TestAnalysisPipeline : IPipelineService
{
    private readonly IFormatDetector formatDetector;
    private readonly IParserFactory parserFactory;
    private readonly IDataValidator dataValidator;
    private readonly IQuestionBatchBuilder batchBuilder;
    private readonly ITestAnalysisAgent testAnalysisAgent;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="TestAnalysisPipeline"/>
    /// </summary>
    public TestAnalysisPipeline(IFormatDetector formatDetector,
        IParserFactory parserFactory,
        IDataValidator dataValidator,
        IQuestionBatchBuilder batchBuilder,
        ITestAnalysisAgent testAnalysisAgent)
    {
        this.formatDetector = formatDetector;
        this.parserFactory = parserFactory;
        this.dataValidator = dataValidator;
        this.batchBuilder = batchBuilder;
        this.testAnalysisAgent = testAnalysisAgent;
    }

    async Task<PipelineResult> IPipelineService.RunAsync(PipelineContext context)
    {
        var userAnswersFormat = formatDetector.DetectFormat(context.UserAnswersFileName, context.UserAnswersStream);
        var userAnswersParser = parserFactory.GetParser(userAnswersFormat, ParsingTarget.UserAnswers);

        var parsedUserAnswers = await userAnswersParser.ParseAsync<List<UserTestResult>>(context.UserAnswersStream);

        var validationResult = dataValidator.Validate(parsedUserAnswers);

        var batches = batchBuilder.Build(validationResult);

        var allQuestionResults = new List<QuestionAnalysisResult>();

        foreach (var batch in batches)
        {
            var needAnalysis = batch.Answers
                .Where(a => a.PreStatus == AnswerPreStatus.NeedAnalysis)
                .ToList();

            var aiResults = new List<ComparisonResult>();
            if (needAnalysis.Count != 0)
            {
                var agentResponse = await testAnalysisAgent.AnalyzeBatchAsync(batch, context.AnalysisMethod == AnalysisMethod.RussianAiAgent
                    ? LlmVariant.Russian
                    : LlmVariant.Foreign);

                aiResults = agentResponse.Results.Select(r => new ComparisonResult
                {
                    UserId = r.UserId,
                    SimilarityPercent = r.SimilarityPercent,
                    Verdict = r.Verdict,
                    Comment = r.Comment
                }).ToList();
            }

            var finalResults = batch.Answers.Select(item =>
            {
                if (item.PreStatus == AnswerPreStatus.ExactMatch)
                {
                    return new ComparisonResult { UserId = item.UserId, SimilarityPercent = 100, Verdict = "correct" };
                }
                else if (item.PreStatus == AnswerPreStatus.Empty)
                {
                    return new ComparisonResult { UserId = item.UserId, SimilarityPercent = 0, Verdict = "incorrect", Comment = "пустой ответ" };
                }
                return aiResults.First(r => r.UserId == item.UserId);
            }).ToList();

            allQuestionResults.Add(new QuestionAnalysisResult
            {
                Question = batch.Question,
                Results = finalResults
            });
        }

        return new PipelineResult()
        {
            AnalysisData = allQuestionResults,
        };
    }
}
