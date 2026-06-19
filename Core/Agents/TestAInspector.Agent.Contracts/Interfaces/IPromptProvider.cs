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
}
