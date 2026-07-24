using TestAInspector.Agent.Contracts.Enums;
using TestAInspector.Agent.Contracts.Models;

namespace TestAInspector.Agent.Contracts.Interfaces;

/// <summary>
/// Клиент для обращение к LLM
/// </summary>
public interface ILlmClient
{
    /// <summary>
    /// Группа LLM
    /// </summary>
    LlmVariant LlmVariant { get; }

    /// <summary>
    /// Отправить запрос 
    /// </summary>
    Task<LlmResponse> SendRequestAsync(LlmRequest request, string targetTest);
}
