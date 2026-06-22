using System.Text.Json.Serialization;

namespace TestAInspector.Agent.DeepSeek.Models;

/// <summary>
/// Результат ллм модели от DeepSeek
/// </summary>
internal class Choice
{
    [JsonPropertyName("message")]
    public ChatMessage Message { get; set; } = null!;
}
