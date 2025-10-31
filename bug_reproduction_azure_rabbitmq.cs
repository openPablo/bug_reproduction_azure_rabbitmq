using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function;

public class RabbitMQTriggerCSharp
{
    private readonly ILogger _logger;

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
                _logger.LogInformation("Metric {key}: {value}", kvp.Key, kvp.Value);
            }
        }
    }
}