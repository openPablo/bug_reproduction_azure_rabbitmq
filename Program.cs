using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;


var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddOpenTelemetry()
        .ConfigureResource(resource => resource.AddService("functionAppName"))
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
