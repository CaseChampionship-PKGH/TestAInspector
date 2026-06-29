using TestAInspector.Entities.Models;
using TestAInspector.Validation.Contracts.Models;

namespace TestAInspector.Validation.Contracts.Interfaces;

/// <summary>
/// Сервис валидации данных перед анализом
/// </summary>
public interface IDataValidator
{
    /// <summary>
    /// Валидировать результаты теста и эталонные ответы
    /// </summary>
    ValidationResult Validate(IEnumerable<UserTestResult> results);
}
