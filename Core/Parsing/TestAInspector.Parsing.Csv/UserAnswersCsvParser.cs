using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using TestAInspector.Entities.Models;
using TestAInspector.Parsing.Contracts.Enums;
using TestAInspector.Parsing.Contracts.Interfaces;

namespace TestAInspector.Parsing.Csv;

/// <summary>
/// Csv парсер тестов пользователей
/// </summary>
public class UserAnswersCsvParser : IDataParser
{
    /// <inheritdoc />
    public InputFormat Format => InputFormat.Csv;

    /// <inheritdoc />
    public ParsingTarget Target => ParsingTarget.UserAnswers;

    /// <inheritdoc />
    public async Task<T> ParseAsync<T>(Stream input)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            Encoding = Encoding.UTF8
        };

        using var reader = new StreamReader(input);
        using var csv = new CsvReader(reader, config);

        csv.Read();
        csv.ReadHeader();
        var headers = csv.HeaderRecord;
        var questionTexts = new List<string>();

        for (var i = 4; i < headers!.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(headers[i]))
            {
                questionTexts.Add(headers[i]);
            }
        }

        var results = new List<UserTestResult>();

        // 2. Обрабатываем строки пользователей
        while (csv.Read())
        {
            var userId = csv.GetField(0);
            if (string.IsNullOrWhiteSpace(userId) || userId.StartsWith(";;;"))
            {
                continue; // пропускаем служебные строки
            }

            var userResult = new UserTestResult
            {
                UserId = userId,
                Date = DateTime.Parse(csv.GetField(1) ?? DateTime.Now.ToString()),
                Status = csv.GetField(2) ?? string.Empty,
                Score = ParseScore(csv.GetField(3) ?? string.Empty),
                Answers = []
            };

            var fieldIdx = 4;
            for (var q = 0; q < questionTexts.Count; q++)
            {
                if (fieldIdx + 3 >= csv.Parser.Count)
                {
                    break;
                }
                var answer = new QuestionAnswer
                {
                    QuestionText = questionTexts[q],
                    Type = csv.GetField(fieldIdx)!,
                    UserAnswer = csv.GetField(fieldIdx + 2)!,
                    CorrectAnswer = csv.GetField(fieldIdx + 3)!
                };
                userResult.Answers.Add(answer);
                fieldIdx += 4;
            }
            results.Add(userResult);
        }

        return (T)(object)results;
    }

    private static int ParseScore(string scoreString)
    {
        var parts = scoreString.Split('/');
        if (parts.Length > 0 && int.TryParse(parts[0].Trim(), out var score))
        {
            return score;
        }
        return 0;
    }
}
