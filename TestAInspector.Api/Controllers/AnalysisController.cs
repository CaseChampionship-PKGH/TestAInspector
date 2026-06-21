using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestAInspector.Analysis.Contracts.Enums;
using TestAInspector.Api.Models;
using TestAInspector.Parsing.Contracts.Exceptions;
using TestAInspector.Reporting.Contracts.Models;
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
    private readonly IMapper mapper;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AnalysisController"/>
    /// </summary>
    public AnalysisController(IPipelineService pipeline, IMapper mapper)
    {
        this.pipeline = pipeline;
        this.mapper = mapper;
    }

    /// <summary>
    /// Запуск анализа и получение структурированного отчёта (JSON).
    /// </summary>
    [HttpPost("run")]
    public async Task<IActionResult> RunAnalysis(
        IFormFile userAnswers,
        IFormFile referenceAnswers,
        [FromQuery] AnalysisMethod analysisMethod = AnalysisMethod.RussianAiAgent,
        [FromQuery] string output = "json")
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
            AnalysisMethod = analysisMethod
        };

        try
        {
            var result = await pipeline.RunAsync(context);
            if (output == "excel")
            {
                return File(result.ExcelReport, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
            }

            var mappedResult = mapper.Map<ReportDataApiModel>(result.ReportData);
            return Ok(mappedResult);
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
    /// Экспорт отчёта в Excel (.xlsx) с подсветкой отклонений.
    /// </summary>
    [HttpPost("export/excel")]
    public IActionResult ExportToExcel([FromBody] ReportData report)
    {
        //var exporter = new ReportExporter(); // из Reporting
        //var bytes = exporter.ExportToExcel(report);
        //return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        return Ok();
    }

    /// <summary>
    /// Проверка работоспособности сервиса.
    /// </summary>
    [HttpGet("health")]
    public IActionResult Health() => Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
}
