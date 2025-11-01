using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Extensions.DependencyInjection;


var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddOpenTelemetry()
        .UseFunctionsWorkerDefaults()
        .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation();
    }).WithMetrics(metrics =>
        {
            metrics
                .AddAspNetCoreInstrumentation()
                .AddRuntimeInstrumentation();
        }).WithLogging().UseOtlpExporter();

builder.Build().Run();
