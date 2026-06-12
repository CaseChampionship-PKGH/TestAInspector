using TestAInspector.Analysis.Contracts.Models;
using TestAInspector.Entities.Models;

namespace TestAInspector.Analysis.Contracts.Interfaces;

/// <summary>
/// Анализатор тестов
/// </summary>
public interface IAnswerAnalyzer
{
    /// <summary>
    /// Проанализировать
    /// </summary>
    Task<ComparisonResult> AnalyzeAsync(ReferenceAnswer reference, TestResult userAnswer);
}
