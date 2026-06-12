using TestAInspector.Agent.Contracts.Models;

namespace TestAInspector.Agent.Contracts.Interfaces;

/// <summary>
/// Клиент для обращение к LLM
/// </summary>
public interface ILlmClient
{
    /// <summary>
    /// Отправить запрос 
    /// </summary>
    Task<LlmResponse> SendRequestAsync(LlmRequest request);
}
