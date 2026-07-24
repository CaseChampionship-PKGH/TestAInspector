using System.Text.Json.Serialization;

namespace TestAInspector.Agent.GigaChat.Models;

/// <summary>
/// Результат ллм модели от Gigachat
/// </summary>
internal class Choice
{
    [JsonPropertyName("message")]
    public ChatMessage Message { get; set; } = null!;
}
