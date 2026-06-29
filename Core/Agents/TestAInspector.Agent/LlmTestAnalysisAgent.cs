using System.Text;
using TestAInspector.Agent.Contracts.Enums;
using TestAInspector.Agent.Contracts.Interfaces;
using TestAInspector.Agent.Contracts.Models;
using TestAInspector.Parsing.Contracts.Enums;
using TestAInspector.Parsing.Contracts.Interfaces;
using TestAInspector.Validation.Contracts.Models.Batch;

namespace TestAInspector.Agent;

/// <inheritdoc cref="ITestAnalysisAgent"/> на базе искуственного интелекта LLM
public class LlmTestAnalysisAgent : ITestAnalysisAgent
{
    private readonly IPromptProvider promptProvider;
    private readonly IParserFactory parserFactory;
    private readonly ILlmFactory llmFactory;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="LlmTestAnalysisAgent"/>
    /// </summary>
    public LlmTestAnalysisAgent(IPromptProvider promptProvider,
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
        }, "analysis");

        var cleanJson = rawResponse.RawResponse
            .Replace("```json", "")
            .Replace("```", "")
            .Trim();

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(cleanJson));
        var parser = parserFactory.GetParser(InputFormat.Json, ParsingTarget.AgentResponse);
        return await parser.ParseAsync<AgentBatchResponse>(stream);
    }
}
