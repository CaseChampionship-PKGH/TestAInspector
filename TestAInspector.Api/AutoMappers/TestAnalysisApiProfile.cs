using AutoMapper;
using TestAInspector.Api.Models;
using TestAInspector.Reporting.Contracts.Models;

namespace TestAInspector.Api.AutoMappers;

/// <inheritdoc />
public class TestAnalysisApiProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="TestAnalysisApiProfile"/>
    /// </summary>
    public TestAnalysisApiProfile()
    {
        CreateMap<ReportDataApiModel, ReportData>(MemberList.Source).ReverseMap();
        CreateMap<SummaryApiModel, Summary>(MemberList.Source).ReverseMap();
        CreateMap<QuestionReportApiModel, QuestionReport>(MemberList.Source).ReverseMap();
    }
}
