namespace TestAInspector.Validation.Contracts.Models;

/// <summary>
/// Результат валидации
/// </summary>
public record ValidationResult
{
    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Успешно ли прошла валидация
    /// </summary>
    public bool Success { get; set; }
}
