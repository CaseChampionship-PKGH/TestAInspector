using TestAInspector.Parsing.Contracts.Enums;

namespace TestAInspector.Parsing.Contracts.Interfaces;

/// <summary>
/// Парсер данных
/// </summary>
public interface IDataParser
{
    /// <summary>
    /// Какой формат парсит
    /// </summary>
    InputFormat Format { get; }

    /// <summary>
    /// Для чего парсит
    /// </summary>
    ParsingTarget Target { get; }

    /// <summary>
    /// Распарсить данные
    /// </summary>
    Task<T> ParseAsync<T>(Stream input);
}
