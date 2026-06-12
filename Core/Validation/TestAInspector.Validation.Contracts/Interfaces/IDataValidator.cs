using System.ComponentModel.DataAnnotations;
using TestAInspector.Entities.Models;

namespace TestAInspector.Validation.Contracts.Interfaces;

/// <summary>
/// Сервис валидации данных перед анализом
/// </summary>
public interface IDataValidator
{
    /// <summary>
    /// Валидировать результаты теста и эталонные ответы
    /// </summary>
    ValidationResult Validate(IEnumerable<TestResult> results, IEnumerable<ReferenceAnswer> references);
}
