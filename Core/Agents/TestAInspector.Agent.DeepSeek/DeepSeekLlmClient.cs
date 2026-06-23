using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TestAInspector.Agent.Contracts.Enums;
using TestAInspector.Agent.Contracts.Interfaces;
using TestAInspector.Agent.Contracts.Models;
using TestAInspector.Agent.DeepSeek.Models;

namespace TestAInspector.Agent.DeepSeek;

/// <inheritdoc cref="ILlmClient"/> на основе DeepSeek
public class DeepSeekLlmClient : ILlmClient
{
    private readonly HttpClient httpClient;
    private readonly string apiKey;
    private readonly string baseUrl;
    private readonly string requestUri;
    private readonly string model;

    LlmVariant ILlmClient.LlmVariant => LlmVariant.Foreign;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DeepSeekLlmClient"/>
    /// </summary>
    public DeepSeekLlmClient(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        httpClient = httpClientFactory.CreateClient("DeepSeek");
        baseUrl = config.GetRequiredSection("ForeignLLM").GetValue<string>("BaseUrl")!;
        apiKey = config.GetRequiredSection("ForeignLLM").GetValue<string>("ApiKey")!;
        requestUri = config.GetRequiredSection("ForeignLLM").GetValue<string>("RequestUri")!;
        model = config.GetRequiredSection("ForeignLLM").GetValue<string>("Model")!;
    }

    async Task<LlmResponse> ILlmClient.SendRequestAsync(LlmRequest llmRequest)
    {
        httpClient.BaseAddress = new Uri(baseUrl);

        var request = new DeepSeekCompatibleRequest
        {
            Model = model,
            Messages =
            [
                new ChatMessage { Role = "system", Content = "Ты — эксперт по проверке тестовых заданий." },
                new ChatMessage { Role = "user", Content = llmRequest.RawPrompt }
            ],
            Temperature = 0,
            MaxTokens = 2000
        };

        var json = JsonSerializer.Serialize(request);
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Api-Key", apiKey);

        var response = await httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<DeepSeekCompatibleResponse>(body);

        return new LlmResponse()
        {
            RawResponse = result?.Choices?.FirstOrDefault()?.Message?.Content
               ?? throw new InvalidOperationException("Пустой ответ от внешнего LLM")
        };
    }
    //    => new()
    //{
    //    RawResponse = @"```json
    //    {
    //      ""results"": [
    //        {
    //          ""userId"": ""user_001"",
    //          ""similarityPercent"": 85,
    //          ""verdict"": ""correct"",
    //          ""comment"": ""Ответ содержит ключевые понятия, но формулировка неполная.""
    //        }
    //      ]
    //    }
    //    ```"
    //};
}
