using TestAInspector.Entities.Models;
using TestAInspector.Parsing.Contracts.Enums;
using TestAInspector.Parsing.Contracts.Interfaces;
using TestAInspector.Services.Contracts.Interfaces;
using TestAInspector.Services.Contracts.Models;
using TestAInspector.Validation.Contracts.Interfaces;

namespace TestAInspector.Services;

/// <summary>
/// <inheritdoc cref="IPipelineService"/>
/// </summary>
public class TestAnalysisPipeline : IPipelineService
{
    private readonly IFormatDetector formatDetector;
    private readonly IParserFactory parserFactory;
    private readonly IDataValidator dataValidator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="TestAnalysisPipeline"/>
    /// </summary>
    public TestAnalysisPipeline(IFormatDetector formatDetector,
        IParserFactory parserFactory,
        IDataValidator dataValidator)
    {
        this.formatDetector = formatDetector;
        this.parserFactory = parserFactory;
        this.dataValidator = dataValidator;
    }

    async Task<PipelineResult> IPipelineService.RunAsync(PipelineContext context)
    {
        var userAnswersFormat = formatDetector.DetectFormat(context.UserAnswersFileName, context.UserAnswersStream);
        var userAnswersParser = parserFactory.GetParser(userAnswersFormat, ParsingTarget.UserAnswers);

        var parsedUserAnswers = await userAnswersParser.ParseAsync<List<UserTestResult>>(context.UserAnswersStream);

        var validatedResult = dataValidator.Validate(parsedUserAnswers);

        return new PipelineResult()
        {
            ValidationResult = validatedResult,
        };
    }
}
