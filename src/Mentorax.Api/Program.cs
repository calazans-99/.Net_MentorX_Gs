
using Microsoft.EntityFrameworkCore;
using Mentorax.Api.Data;
using Mentorax.Api.Common;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Serilog bootstrap
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services
builder.Services.AddControllers();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Mentorax.Api.Services.Mappings.MappingProfile));

// Repositories registration
builder.Services.AddScoped<Mentorax.Api.Repositories.Interfaces.IMentorRepository, Mentorax.Api.Repositories.Implementations.MentorRepository>();
builder.Services.AddScoped<Mentorax.Api.Repositories.Interfaces.IMentoradoRepository, Mentorax.Api.Repositories.Implementations.MentoradoRepository>();
builder.Services.AddScoped<Mentorax.Api.Repositories.Interfaces.IQuestionarioRepository, Mentorax.Api.Repositories.Implementations.QuestionarioRepository>();
builder.Services.AddScoped<Mentorax.Api.Repositories.Interfaces.IPlanoMentoriaRepository, Mentorax.Api.Repositories.Implementations.PlanoMentoriaRepository>();
builder.Services.AddScoped<Mentorax.Api.Repositories.Interfaces.ITarefaMentoriaRepository, Mentorax.Api.Repositories.Implementations.TarefaMentoriaRepository>();

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});
builder.Services.AddVersionedApiExplorer();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HealthChecks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>("db");

// DbContext
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(conn))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(conn));
}

// Configure ApiKey and Validators
builder.Services.AddSingleton<IApiKeyValidator, Mentorax.Api.Middleware.ApiKeyValidator>();


// Services registration (if any)
builder.Services.AddScoped<Mentorax.Api.Services.PlanoMentoriaService>();
builder.Services.AddScoped<Mentorax.Api.Services.PdfService>();
builder.Services.AddScoped<Mentorax.Api.Services.AIClient>();
builder.Services.AddScoped<Mentorax.Api.Security.Interfaces.IApiKeyValidator, Mentorax.Api.Security.Implementations.ApiKeyValidator>();

var app = builder.Build();


app.UseGlobalExceptionHandling();
// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mentorax API V1");
    });
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

// ApiKey middleware (expects ApiKeyMiddleware implementation in Middleware folder)
app.UseMiddleware<Mentorax.Api.Middleware.ApiKeyMiddleware>();

app.UseAuthorization();

app.MapHealthChecks("/health/ready");
app.MapHealthChecks("/health/live");

app.MapControllers();

app.Run();
