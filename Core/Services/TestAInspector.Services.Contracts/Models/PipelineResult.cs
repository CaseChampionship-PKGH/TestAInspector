using TestAInspector.Analysis.Contracts.Models;
using TestAInspector.Entities.Models;

namespace TestAInspector.Services.Contracts.Models;

/// <summary>
/// Результат выполнения pipeline
/// </summary>
public record PipelineResult
{
    /// <summary>
    /// Результат анализа
    /// </summary>
    public AnalysisData AnalysisData { get; set; } = null!;

    /// <summary>
    /// Отчёт
    /// </summary>
    public ReportData ReportData { get; set; } = null!;

    /// <summary>
    /// Распарсенные данные (для теста, потом удалить)
    /// </summary>
    public List<UserTestResult> Parsed { get; set; } = null!;
}
