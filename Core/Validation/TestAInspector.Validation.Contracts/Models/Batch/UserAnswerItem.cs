using TestAInspector.Validation.Contracts.Models.Batch.Enums;

namespace TestAInspector.Validation.Contracts.Models.Batch;

/// <summary>
/// Сжатое представление вопроса для анализа
/// </summary>
public class UserAnswerItem
{
    /// <summary>
    /// Идентфикатор пользователя
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Исходный ответ пользователя
    /// </summary>
    public string RawAnswer { get; set; } = string.Empty;

    /// <summary>
    /// Результат предварительной проверки
    /// </summary>
    public AnswerPreStatus PreStatus { get; set; }
}
