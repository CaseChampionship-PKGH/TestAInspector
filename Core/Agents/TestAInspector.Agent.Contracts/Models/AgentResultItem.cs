namespace TestAInspector.Agent.Contracts.Models;

/// <summary>
/// Сведения о ответе пользователя
/// </summary>
public class AgentResultItem
{
    /// <summary>
    /// Идентфикатор пользователя
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Процент схожести
    /// </summary>
    public double SimilarityPercent { get; set; }

    /// <summary>
    /// Вердикт ("correct", "partial", "incorrect")
    /// </summary>
    public string Verdict { get; set; } = string.Empty;

    /// <summary>
    /// Комментарий
    /// </summary>
    public string Comment { get; set; } = string.Empty;
}
