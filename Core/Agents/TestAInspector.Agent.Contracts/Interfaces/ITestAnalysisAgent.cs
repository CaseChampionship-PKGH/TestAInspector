using TestAInspector.Analysis.Contracts.Models;
using TestAInspector.Entities.Models;

namespace TestAInspector.Agent.Contracts.Interfaces;

/// <summary>
/// ИИ-Агент для анализа правильности ответа
/// </summary>
public interface ITestAnalysisAgent
{
    /// <summary>
    /// Сравнить правильность ответа пользователя и эталона 
    /// </summary>
    Task<ComparisonResult> CompareAsync(ReferenceAnswer reference, TestResult userAnswer);
}
