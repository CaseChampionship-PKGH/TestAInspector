using TestAInspector.Reporting.Contracts.Models;
using TestAInspector.Validation.Contracts.Models.Batch;

namespace TestAInspector.Agent.Contracts.Interfaces;

/// <summary>
/// Помошник в преобразовании данных в промпт
/// </summary>
public interface IPromptProvider
{
    /// <summary>
    /// Преобразовать данные одного вопроса в промпт анализа
    /// </summary>
    string BuildBatchAnalysisPrompt(QuestionBatch batch);

    /// <summary>
    /// Преобразовать данные одного вопроса в промпт создания отчёта
    /// </summary>
    string BuildReportGeneratingPrompt(Summary summary, List<string> criticalIssues, List<QuestionReport> questions);
}
