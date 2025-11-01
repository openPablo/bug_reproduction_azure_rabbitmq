using System.Diagnostics;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace bug_reproduction_azure_rabbitmq;

public class RabbitMQTriggerCSharp
{
    private readonly ILogger _logger;
    private int totalMessages;
    private double totalTime;
    private static readonly object _lock = new();
    public RabbitMQTriggerCSharp(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<RabbitMQTriggerCSharp>();
    }

    [Function("bugreproducer")]
    public void Run([RabbitMQTrigger("metrics", ConnectionStringSetting = "RabbitMQConnection")] string myQueueItem)
    {
        
        var sw = Stopwatch.StartNew();
        var metrics = JsonSerializer.Deserialize<Dictionary<string, object>>(myQueueItem);
        if (metrics != null)
        {
            foreach (var kvp in metrics)
            {
                _logger.LogInformation("Metric {key}: {value}", kvp.Key, kvp.Value);
            }
        }
        sw.Stop();
        lock(_lock)
        {
            totalMessages++;
            totalTime += sw.Elapsed.TotalSeconds;
        }

        if (totalMessages % 10 == 0)
        {
            _logger.LogInformation("Average processing time: {avgTime:F4}s over {count} messages", totalTime / totalMessages, totalMessages);
        }
    }
}

