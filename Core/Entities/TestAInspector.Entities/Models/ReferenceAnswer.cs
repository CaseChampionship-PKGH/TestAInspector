namespace TestAInspector.Entities.Models;

/// <summary>
/// Эталонный ответ
/// </summary>
public class ReferenceAnswer
{
    /// <summary>
    /// Идентификатор вопроса
    /// </summary>
    public string QuestionId { get; set; } = string.Empty;

    /// <summary>
    /// Текст вопроса
    /// </summary>
    public string QuestionText { get; set; } = string.Empty;

    /// <summary>
    /// Правильный ответ
    /// </summary>
    public string CorrectAnswer { get; set; } = string.Empty;

    /// <summary>
    /// Тип задания
    /// </summary>
    public string Type { get; set; } = string.Empty;
}
