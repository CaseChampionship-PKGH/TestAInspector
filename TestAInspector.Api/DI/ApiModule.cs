using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using TestAInspector.Agent;
using TestAInspector.Agent.Contracts.Interfaces;
using TestAInspector.Agent.GigaChat;
using TestAInspector.Agent.OpenAI;
using TestAInspector.Agent.YandexGPT;
using TestAInspector.Api.AutoMappers;
using TestAInspector.Common.Mvc.Extensions;
using TestAInspector.Parsing;
using TestAInspector.Parsing.Archive;
using TestAInspector.Parsing.Contracts.Interfaces;
using TestAInspector.Parsing.Csv;
using TestAInspector.Parsing.Json;
using TestAInspector.Reporting;
using TestAInspector.Services;
using TestAInspector.Validation;
using Module = TestAInspector.Common.Mvc.Module;

namespace TestAInspector.Api.DI;

/// <inheritdoc />
public class ApiModule : Module
{
    /// <inheritdoc />
    protected override void Load(IServiceCollection services)
    {
        var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddSingleton<UserAnswersCsvParser>();
        services.AddSingleton<UserAnswersJsonParser>();
        services.AddSingleton<UserAnswersArchiveParser>();
        services.RegisterMultipleInterfacesAssignableTo<IDataParser, UserAnswersCsvParser>(ServiceLifetime.Singleton);
        services.RegisterMultipleInterfacesAssignableTo<IDataParser, UserAnswersJsonParser>(ServiceLifetime.Singleton);
        services.RegisterMultipleInterfacesAssignableTo<IDataParser, UserAnswersArchiveParser>(ServiceLifetime.Singleton);
        services.RegisterMultipleInterfacesAssignableTo<IDataParser, AgentResponseJsonParser>(ServiceLifetime.Singleton);

        {
        });

        {
        });
        services.RegisterMultipleInterfacesAssignableTo<ILlmClient, OpenAiCompatibleLlmClient>(ServiceLifetime.Singleton);

        services.RegisterAsImplementedInterfaces<ParserFactory>(ServiceLifetime.Singleton);
        services.RegisterAsImplementedInterfaces<FormatDetector>(ServiceLifetime.Singleton);
        services.RegisterAsImplementedInterfaces<ExcelReportExporter>(ServiceLifetime.Singleton);
        services.RegisterAsImplementedInterfaces<TestAnalysisPipeline>(ServiceLifetime.Singleton);
        services.RegisterAsImplementedInterfaces<DataValidator>(ServiceLifetime.Singleton);
        services.RegisterAsImplementedInterfaces<QuestionBatchBuilder>(ServiceLifetime.Singleton);
        services.RegisterAsImplementedInterfaces<DefaultPromptProvider>(ServiceLifetime.Singleton);
        services.RegisterAsImplementedInterfaces<LlmFactory>(ServiceLifetime.Singleton);
        services.RegisterAsImplementedInterfaces<LlmTestAnalysisAgent>(ServiceLifetime.Singleton);
        services.RegisterAsImplementedInterfaces<LlmReportAgent>(ServiceLifetime.Singleton);
        services.RegisterAsImplementedInterfaces<AgentReportBuilder>(ServiceLifetime.Singleton);
        services.RegisterAutoMapperProfile<TestAnalysisApiProfile>();
        RegisterAutoMapper(services);
        services.AddHttpContextAccessor();
    }

    private static void RegisterAutoMapper(IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            var profiles = provider.GetServices<Profile>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                foreach (var profile in profiles)
                {
                    mc.AddProfile(profile);
                }
            }, NullLoggerFactory.Instance);
            var mapper = mapperConfig.CreateMapper();
            return mapper;
        });
    }
}
