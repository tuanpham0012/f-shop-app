using Microsoft.Extensions.Logging;
using ShopAppApi.Job;
using ShopAppApi.Services.Metrics;
using System.Diagnostics;

namespace ShopAppApi.Middlewares
{
    public class GlobalRoutePrefixMiddleware(RequestDelegate next, ICoreMonitoringData coreMonitoringData, ILogger<HealthCheckJobInvocable> logger, string routePrefix)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.PathBase = new PathString(routePrefix);

            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;


            await next(context);


            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;

            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;

            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
            coreMonitoringData.WriteData("worker_task", "cpu", cpuUsageTotal*100);

            logger.LogInformation("Schedule run at: {time}, value {value}", DateTimeOffset.Now, totalMsPassed);

            
        }
    }
}
