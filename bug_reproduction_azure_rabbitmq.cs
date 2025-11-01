using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace bug_reproduction_azure_rabbitmq;

public class RabbitMQTriggerCSharp
{
    private readonly ILogger _logger;

    private static readonly object _lock = new();
    public RabbitMQTriggerCSharp(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<RabbitMQTriggerCSharp>();
    }

    [Function("bugreproducer")]
    public void Run([RabbitMQTrigger("metrics", ConnectionStringSetting = "RabbitMQConnection")] string myQueueItem)
    {
        
        var metrics = JsonSerializer.Deserialize<Dictionary<string, object>>(myQueueItem);
        if (metrics != null)
        {
            foreach (var kvp in metrics)
            {
                //_logger.LogWarning("Metric {key}: {value}", kvp.Key, kvp.Value);
            }
        }
    }
}

