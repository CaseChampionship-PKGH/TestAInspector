using TestAInspector.Analysis.Contracts.Enums;
using TestAInspector.Analysis.Contracts.Models;
using TestAInspector.Reporting.Contracts.Models;

namespace TestAInspector.Reporting.Contracts.Interfaces;

/// <summary>
/// Генератор отчёта
/// </summary>
public interface IReportBuilder
{
    /// <summary>
    /// Построить отчёт по анализам
    /// </summary>
    Task<ReportData> BuildAsync(List<QuestionAnalysisResult> data, AnalysisMethod analysisMethod);
}
