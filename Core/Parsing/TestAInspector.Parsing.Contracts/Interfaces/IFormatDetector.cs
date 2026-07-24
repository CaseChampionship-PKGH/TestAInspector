using TestAInspector.Parsing.Contracts.Enums;

namespace TestAInspector.Parsing.Contracts.Interfaces;

/// <summary>
/// Резолвер формата данных
/// </summary>
public interface IFormatDetector
{
    /// <summary>
    /// Вычислить тип данных
    /// </summary>
    InputFormat DetectFormat(string fileName, Stream content);
}
