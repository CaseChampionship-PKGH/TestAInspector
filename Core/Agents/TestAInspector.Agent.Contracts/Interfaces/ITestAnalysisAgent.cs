using TestAInspector.Agent.Contracts.Enums;
using TestAInspector.Agent.Contracts.Models;
using TestAInspector.Validation.Contracts.Models.Batch;

namespace TestAInspector.Agent.Contracts.Interfaces;

/// <summary>
/// ИИ-Агент для анализа правильности ответа
/// </summary>
public interface ITestAnalysisAgent
{
    /// <summary>
    /// Сравнить правильность ответа тестируемого
    /// </summary>
    Task<AgentBatchResponse> AnalyzeBatchAsync(QuestionBatch questionBatch, LlmVariant llmVariant);
}
