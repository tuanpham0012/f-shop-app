using Coravel.Invocable;
using ShopAppApi.Repositories.Metrics;
using System.Diagnostics;

namespace ShopAppApi.Job
{
    public class HealthCheckJobInvocable(ICoreMonitoringData coreMonitoringData, ILogger<HealthCheckJobInvocable> logger) : IInvocable
    {
        public async Task Invoke()
        {
            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            await Task.Delay(500);
            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;

            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;

            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
            coreMonitoringData.WriteTimeMetric("worker_task", "product", (long)(cpuUsageTotal * 100));

            logger.LogInformation("Schedule run at: {time}, value {value}", DateTimeOffset.Now, cpuUsageTotal * 100);

        }
    }
}
