using System.Text.Json.Serialization;

namespace TestAInspector.Agent.YandexGPT.Models;

/// <summary>
/// Ответ ллм модели от YandexGPT
/// </summary>
internal class YandexGptResponse
{
    [JsonPropertyName("result")]
    public required Result Result { get; set; }
}
