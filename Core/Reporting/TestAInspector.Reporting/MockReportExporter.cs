using TestAInspector.Analysis.Contracts.Models;
using TestAInspector.Reporting.Contracts.Interfaces;

namespace TestAInspector.Reporting;

/// <summary>
/// <inheritdoc cref="IReportExporter"/>
/// </summary>
public class MockReportExporter : IReportExporter
{
    byte[] IReportExporter.ExportToExcel(ReportData report) => throw new NotImplementedException();
}
