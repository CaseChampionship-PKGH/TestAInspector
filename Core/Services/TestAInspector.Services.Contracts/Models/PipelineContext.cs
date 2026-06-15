using TestAInspector.Analysis.Contracts.Enums;

namespace TestAInspector.Services.Contracts.Models;

/// <summary>
/// Результат выполнения pipeline
/// </summary>
public record PipelineContext
{
    /// <summary>
    /// Результат анализа
    /// </summary>
    public Stream UserAnswersStream { get; set; } = null!;

    /// <summary>
    /// Имя файла с тестом
    /// </summary>
    public string UserAnswersFileName { get; set; } = string.Empty;

    /// <summary>
    /// Результат анализа
    /// </summary>
    public Stream ReferenceAnswersStream { get; set; } = null!;

    /// <summary>
    /// Имя файла с эталонными ответами
    /// </summary>
    public string ReferenceAnswersFileName { get; set; } = string.Empty;

    /// <summary>
    /// Метод аналиа
    /// </summary>
    public AnalysisMethod AnalysisMethod { get; set; }
}
