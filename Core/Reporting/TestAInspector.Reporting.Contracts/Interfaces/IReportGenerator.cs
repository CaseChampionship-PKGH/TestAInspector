using TestAInspector.Analysis.Contracts.Models;

namespace TestAInspector.Reporting.Contracts.Interfaces;

/// <summary>
/// Генератор отчёта
/// </summary>
public interface IReportGenerator
{
    /// <summary>
    /// Сгенерировать отчёт по анализам
    /// </summary>
    Task<ReportData> GenerateAsync(List<QuestionAnalysisResult> data);
}
