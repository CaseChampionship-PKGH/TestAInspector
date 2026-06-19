using System.Text;
using TestAInspector.Agent.Contracts.Enums;
using TestAInspector.Agent.Contracts.Interfaces;
using TestAInspector.Agent.Contracts.Models;
using TestAInspector.Parsing.Contracts.Enums;
using TestAInspector.Parsing.Contracts.Interfaces;
using TestAInspector.Validation.Contracts.Models.Batch;

namespace TestAInspector.Agent;

/// <summary>
/// <inheritdoc cref="ITestAnalysisAgent"/>
/// </summary>
public class OpenAiTestAnalysisAgent : ITestAnalysisAgent
{
    private readonly IPromptProvider promptProvider;
    private readonly IParserFactory parserFactory;
    private readonly ILlmFactory llmFactory; // абстракция над HttpClient/OpenAI SDK

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="OpenAiTestAnalysisAgent"/>
    /// </summary>
    public OpenAiTestAnalysisAgent(IPromptProvider promptProvider,
        IParserFactory parserFactory,
        ILlmFactory llmFactory)
    {
        this.promptProvider = promptProvider;
        this.parserFactory = parserFactory;
        this.llmFactory = llmFactory;
    }

    async Task<AgentBatchResponse> ITestAnalysisAgent.AnalyzeBatchAsync(QuestionBatch questionBatch, LlmVariant llmVariant)
    {
        var prompt = promptProvider.BuildBatchAnalysisPrompt(questionBatch);
        var llmClient = llmFactory.CreateLLmClient(llmVariant);
        var rawResponse = await llmClient.SendRequestAsync(new LlmRequest()
        {
            RawPrompt = prompt
        });

        var cleanJson = rawResponse.RawResponse
            .Replace("```json", "")
            .Replace("```", "")
            .Trim();

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(cleanJson));
        var parser = parserFactory.GetParser(InputFormat.Json, ParsingTarget.AgentResponse);
        return await parser.ParseAsync<AgentBatchResponse>(stream);
    }
}
