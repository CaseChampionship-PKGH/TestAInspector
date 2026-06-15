using Microsoft.AspNetCore.Mvc;
using TestAInspector.Analysis.Contracts.Enums;
using TestAInspector.Parsing.Contracts.Exceptions;
using TestAInspector.Reporting.Contracts.Interfaces;
using TestAInspector.Services.Contracts.Interfaces;
using TestAInspector.Services.Contracts.Models;

namespace TestAInspector.Api.Controllers;

/// <summary>
/// Управление анализом тестов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly IPipelineService pipeline;
    private readonly IReportExporter reportExporter;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AnalysisController"/>
    /// </summary>
    public AnalysisController(IPipelineService pipeline, IReportExporter reportExporter)
    {
        this.pipeline = pipeline;
        this.reportExporter = reportExporter;
    }

    /// <summary>
    /// Запуск анализа и получение структурированного отчёта (JSON).
    /// </summary>
    [HttpPost("run")]
    public async Task<IActionResult> RunAnalysis(
        IFormFile userAnswers,
        IFormFile referenceAnswers,
        [FromForm] AnalysisMethod analysisMethod = AnalysisMethod.RussianAiAgent)
    {
        if (userAnswers == null || userAnswers.Length == 0)
        {
            return BadRequest("Файл с ответами пользователей обязателен.");
        }
        if (referenceAnswers == null || referenceAnswers.Length == 0)
        {
            return BadRequest("Файл с эталонными ответами обязателен.");
        }

        // Формирование контекста конвейера
        var context = new PipelineContext
        {
            UserAnswersStream = userAnswers.OpenReadStream(),
            UserAnswersFileName = userAnswers.FileName,
            ReferenceAnswersStream = referenceAnswers.OpenReadStream(),
            ReferenceAnswersFileName = referenceAnswers.FileName,
            AnalysisMethod = analysisMethod
        };

        try
        {
            var result = await pipeline.RunAsync(context);
            return Ok(result);
        }
        catch (ParsingException ex)
        {
            return UnprocessableEntity(new { error = ex.Message, details = ex.Errors });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Внутренняя ошибка сервера.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Запуск анализа и экспорт отчёта в Excel (.xlsx) с подсветкой отклонений.
    /// </summary>
    [HttpPost("export/excel")]
    public async Task<IActionResult> ExportExcel(
        IFormFile userAnswers,
        IFormFile referenceAnswers,
        [FromForm] AnalysisMethod analysisMethod = AnalysisMethod.RussianAiAgent)
    {
        if (userAnswers == null || userAnswers.Length == 0)
        {
            return BadRequest("Файл с ответами пользователей обязателен.");
        }
        if (referenceAnswers == null || referenceAnswers.Length == 0)
        {
            return BadRequest("Файл с эталонными ответами обязателен.");
        }

        var context = new PipelineContext
        {
            UserAnswersStream = userAnswers.OpenReadStream(),
            UserAnswersFileName = userAnswers.FileName,
            ReferenceAnswersStream = referenceAnswers.OpenReadStream(),
            ReferenceAnswersFileName = referenceAnswers.FileName,
            AnalysisMethod = analysisMethod
        };

        try
        {
            var result = await pipeline.RunAsync(context);
            var excelBytes = reportExporter.ExportToExcel(result.ReportData);
            return File(excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"test-analysis-{DateTime.Now:yyyy-MM-dd}.xlsx");
        }
        catch (ParsingException ex)
        {
            return UnprocessableEntity(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ошибка при формировании Excel-отчёта." });
        }
    }

    /// <summary>
    /// Проверка работоспособности сервиса.
    /// </summary>
    [HttpGet("health")]
    public IActionResult Health() => Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
}
