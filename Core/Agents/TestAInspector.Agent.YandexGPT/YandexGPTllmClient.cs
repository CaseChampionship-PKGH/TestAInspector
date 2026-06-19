using TestAInspector.Agent.Contracts.Enums;
using TestAInspector.Agent.Contracts.Interfaces;
using TestAInspector.Agent.Contracts.Models;

namespace TestAInspector.Agent.YandexGPT;

/// <summary>
/// <inheritdoc cref="ILlmClient"/> на основе YandexGPT
/// </summary>
public class YandexGPTllmClient : ILlmClient
{
    LlmVariant ILlmClient.LlmVariant => LlmVariant.Russian;

    async Task<LlmResponse> ILlmClient.SendRequestAsync(LlmRequest request) => new LlmResponse()
    {
        RawResponse = "бип боп"
    };
}
