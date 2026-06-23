using System.Text;
using TestAInspector.Api.DI;
using TestAInspector.Api.Filters;
using TestAInspector.Common.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterModule<ApiModule>();

builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<EnumSchemaFilter>();
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TestAInspector.Api.xml"));
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TestAInspector.Entities.xml"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
