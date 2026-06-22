using TestAInspector.Validation.Contracts.Models;
using TestAInspector.Validation.Contracts.Models.Batch;

namespace TestAInspector.Validation.Contracts.Interfaces;

/// <summary>
/// Cервис-преобразователь данных для анализа
/// </summary>
public interface IQuestionBatchBuilder
{
    /// <summary>
    /// Перестроить данные от «тестируемый-центричного» вида к «вопрос-центричным» батчам,
    /// попутно выполнив предварительную классификацию ответов (ExactMatch / Empty / NeedAnalysis
    /// </summary>
    List<QuestionBatch> Build(ValidationResult validationResult);
}
