namespace TestAInspector.Entities.Models;

/// <summary>
/// Результат прохождения теста
/// </summary>
public class TestResult
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Идентфикатор вопроса
    /// </summary>
    public string QuestionId { get; set; } = string.Empty;

    /// <summary>
    /// Текст вопроса
    /// </summary>
    public string QuestionText { get; set; } = string.Empty;

    /// <summary>
    /// Ответ пользователя
    /// </summary>
    public string UserAnswer { get; set; } = string.Empty;

    /// <summary>
    /// Время начала
    /// </summary>
    public DateTime StartTime { get; set; }

    // Дополнительные метаданные
}
