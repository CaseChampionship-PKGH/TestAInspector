using TestAInspector.Agent.Contracts.Enums;

namespace TestAInspector.Agent.Contracts.Interfaces;

/// <summary>
/// Фабрика получения агентов
/// </summary>
public interface IAgentFactory
{
    /// <summary>
    /// Получить агента для анализа правильности ответа
    /// </summary>
    ITestAnalysisAgent CreateAnalysisAgent(AgentVariant variant);

    /// <summary>
    /// Получить агента для отчёта
    /// </summary>
    IReportAgent CreateReportAgent(AgentVariant variant);
}
