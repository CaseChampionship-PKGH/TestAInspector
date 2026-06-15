using TestAInspector.Services.Contracts.Models;

namespace TestAInspector.Services.Contracts.Interfaces;

/// <summary>
/// Сервис - оркестратор: вызывает последовательно парсинг, валидацию, временной анализ, агентов сравнения, сбор статистики и генерацию отчёта.
/// </summary>
public interface IPipelineService
{
    /// <summary>
    /// Валидировать результаты теста
    /// </summary>
    Task<PipelineResult> RunAsync(PipelineContext context);
}
