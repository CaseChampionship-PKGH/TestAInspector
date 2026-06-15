namespace TestAInspector.Entities.Models;

/// <summary>
/// Результат прохождения теста
/// </summary>
public class UserTestResult
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Время начала
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Статус теста (пройден/не пройден)
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Количество баллов
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Ответы пользователя
    /// </summary>
    public List<QuestionAnswer> Answers { get; set; } = null!;
}
