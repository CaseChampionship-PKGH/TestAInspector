namespace TestAInspector.Parsing.Contracts.Enums;

/// <summary>
/// Цель парсинга
/// </summary>
public enum ParsingTarget
{
    /// <summary>
    /// Тесты пользователей
    /// </summary>
    UserAnswers,

    /// <summary>
    /// Эталонные ответы
    /// </summary>
    ReferenceAnswers,

    /// <summary>
    /// Ответы от LLM-агентов
    /// </summary>
    AgentResponse
}
