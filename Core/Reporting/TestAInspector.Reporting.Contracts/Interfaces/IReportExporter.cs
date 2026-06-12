using TestAInspector.Analysis.Contracts.Models;

namespace TestAInspector.Reporting.Contracts.Interfaces;

/// <summary>
/// Экспортер отчёта
/// </summary>
public interface IReportExporter
{
    /// <summary>
    /// Экспортировать
    /// </summary>
    byte[] ExportToExcel(ReportData report);
}
