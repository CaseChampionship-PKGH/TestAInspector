using TestAInspector.Agent.Contracts.Enums;
using TestAInspector.Agent.Contracts.Interfaces;
using TestAInspector.Agent.Contracts.Models;
using TestAInspector.Reporting.Contracts.Models;

namespace TestAInspector.Agent;

/// <inheritdoc cref="IReportAgent"/> на базе Open AI
public class OpenAiReportAgent : IReportAgent
{
    private readonly IPromptProvider promptProvider;
    private readonly ILlmFactory llmFactory; // абстракция над HttpClient/OpenAI SDK

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="OpenAiReportAgent"/>
    /// </summary>
    public OpenAiReportAgent(IPromptProvider promptProvider,
        ILlmFactory llmFactory)
    {
        this.promptProvider = promptProvider;
        this.llmFactory = llmFactory;
    }

    async Task<ReportData> IReportAgent.GenerateReportAsync(Summary summary, List<string> criticalIssues, List<QuestionReport> questions, LlmVariant llmVariant)
    {
        var prompt = promptProvider.BuildReportGeneratingPrompt(summary, criticalIssues, questions);
        var llmClient = llmFactory.CreateLLmClient(llmVariant);
        var recommendations = await llmClient.SendRequestAsync(new LlmRequest()
        {
            RawPrompt = prompt
        });

        return new ReportData
        {
            Summary = summary,
            Questions = questions,
            CriticalIssues = criticalIssues,
            Recommendations = recommendations.RawResponse
        };
    }
}
