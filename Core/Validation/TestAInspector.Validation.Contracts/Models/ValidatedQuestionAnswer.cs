namespace TestAInspector.Validation.Contracts.Models;

/// <summary>
/// Ответ на вопрос
/// </summary>
public class ValidatedQuestionAnswer
{
    /// <summary>
    /// Ключ вопроса в QuestionCatalog (QuestionText|CorrectAnswer)
    /// </summary>
    public string QuestionKey { get; set; } = string.Empty;

    /// <summary>
    /// Ответ пользователя
    /// </summary>
    public string UserAnswer { get; set; } = string.Empty;
}
