using TestAInspector.Common.Mvc.Extensions;
using TestAInspector.Parsing;
using TestAInspector.Parsing.Contracts.Interfaces;
using TestAInspector.Parsing.Csv;
using TestAInspector.Reporting;
using TestAInspector.Services;
using TestAInspector.Validation;
using Module = TestAInspector.Common.Mvc.Module;

namespace TestAInspector.Api.DI
{
    /// <inheritdoc />
    public class ApiModule : Module
    {
        /// <inheritdoc />
        protected override void Load(IServiceCollection services)
        {
            services.RegisterMultipleInterfacesAssignableTo<IDataParser, UserAnswersCsvParser>(ServiceLifetime.Singleton);
            services.RegisterAsImplementedInterfaces<ParserFactory>(ServiceLifetime.Singleton);
            services.RegisterAsImplementedInterfaces<FormatDetector>(ServiceLifetime.Singleton);
            services.RegisterAsImplementedInterfaces<MockReportExporter>(ServiceLifetime.Singleton);
            services.RegisterAsImplementedInterfaces<TestAnalysisPipeline>(ServiceLifetime.Singleton);
            services.RegisterAsImplementedInterfaces<DataValidator>(ServiceLifetime.Singleton);
            services.RegisterAsImplementedInterfaces<QuestionBatchBuilder>(ServiceLifetime.Singleton);
            services.AddHttpContextAccessor();
        }
    }
}
