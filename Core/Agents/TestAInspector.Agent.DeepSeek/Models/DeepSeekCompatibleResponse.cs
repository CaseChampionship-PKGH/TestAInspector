using System.Text.Json.Serialization;

namespace TestAInspector.Agent.DeepSeek.Models;

/// <summary>
/// Ответ ллм модели от DeepSeek
/// </summary>
internal class DeepSeekCompatibleResponse
{
    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; } = null!;
}
