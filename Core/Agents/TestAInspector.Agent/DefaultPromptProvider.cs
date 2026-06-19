using System.Text;
using TestAInspector.Agent.Contracts.Interfaces;
using TestAInspector.Validation.Contracts.Models.Batch;

namespace TestAInspector.Agent;

/// <summary>
/// <inheritdoc cref="IPromptProvider"/>
/// </summary>
public class DefaultPromptProvider : IPromptProvider
{
    string IPromptProvider.BuildBatchAnalysisPrompt(QuestionBatch batch)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Ты — эксперт по проверке тестовых заданий.");
        sb.AppendLine("Дан вопрос, эталонный ответ и список ответов пользователей.");
        sb.AppendLine("Для каждого пользователя определи:");
        sb.AppendLine("- процент совпадения с эталоном (0-100)");
        sb.AppendLine("- вердикт: correct / partial / incorrect");
        sb.AppendLine("- краткое обоснование (1 предложение на русском)");
        sb.AppendLine();
        sb.AppendLine($"Вопрос: {batch.Question.QuestionText}");
        sb.AppendLine($"Тип вопроса: {batch.Question.Type}");
        sb.AppendLine($"Правильный ответ: {batch.Question.CorrectAnswer}");
        sb.AppendLine("Ответы пользователей:");

        foreach (var answer in batch.Answers)
        {
            sb.AppendLine($"{answer.UserId}: \"{answer.RawAnswer}\"");
        }

        sb.AppendLine();
        sb.AppendLine("Верни ТОЛЬКО JSON без markdown-обёртки, без пояснений:");
        sb.AppendLine("{");
        sb.AppendLine("  \"results\": [");
        sb.AppendLine("    { \"userId\": \"user_001\", \"similarityPercent\": 85, \"verdict\": \"correct\", \"comment\": \"...\" },");
        sb.AppendLine("    ...");
        sb.AppendLine("  ]");
        sb.AppendLine("}");

        return sb.ToString();
    }
}

