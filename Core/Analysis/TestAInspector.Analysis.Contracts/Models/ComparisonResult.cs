namespace TestAInspector.Analysis.Contracts.Models;

/// <summary>
/// Результат сравнения тестирования
/// </summary>
public class ComparisonResult
{
    /// <summary>
    /// Идентификатор вопроса
    /// </summary>
    public string QuestionId { get; set; } = string.Empty;

    /// <summary>
    /// Правильность "correct" | "partial" | "incorrect"
    /// </summary>
    public string Correctness { get; set; } = string.Empty;

    /// <summary>
    /// Процент равности
    /// </summary>
    public double SimilarityPercent { get; set; }

    /// <summary>
    /// Комментарий
    /// </summary>
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Отличия
    /// </summary>
    public List<string> Differences { get; set; } = null!;
}
