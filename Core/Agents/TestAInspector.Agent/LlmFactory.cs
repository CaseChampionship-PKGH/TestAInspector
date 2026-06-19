using TestAInspector.Agent.Contracts.Enums;
using TestAInspector.Agent.Contracts.Interfaces;
using TestAInspector.Parsing.Contracts.Exceptions;

namespace TestAInspector.Agent;

/// <summary>
/// <inheritdoc cref="ILlmFactory"/>
/// </summary>
public class LlmFactory : ILlmFactory
{
    private readonly Dictionary<LlmVariant, ILlmClient> llmClients;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="LlmFactory"/>
    /// </summary>
    public LlmFactory(IEnumerable<ILlmClient> llmClients)
    {
        this.llmClients = llmClients.ToDictionary(
            p => p.LlmVariant,
            p => p
        );
    }

    ILlmClient ILlmFactory.CreateLLmClient(LlmVariant variant)
    {
        if (llmClients.TryGetValue(variant, out var parser))
        {
            return parser;
        }

        throw new ParsingException($"ИИ-агент группы {variant} не найден.");
    }
}
