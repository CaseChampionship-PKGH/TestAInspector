using TestAInspector.Analysis.Contracts.Models;

namespace TestAInspector.Agent.Contracts.Interfaces;

/// <summary>
/// ИИ-Агент для анализа правильности ответа
/// </summary>
public interface IReportAgent
{
    /// <summary>
    /// Сгенерировать отчёт по данным анализов 
    /// </summary>
    Task<ReportData> GenerateReportAsync(AnalysisData analysisData);
}
