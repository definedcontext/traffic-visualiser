using System;
using System.Threading.Tasks;
using Dctx.TrafficEndpoint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Dctx.TrafficEndpoint
{
    public static class Functions
    {
        public static bool IsHealthy = true;

        [FunctionName("health")]
        public static async Task<IActionResult> Health(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] object message,
            [SignalR(HubName = "simulator")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            var healthy = IsHealthy; // copy now.

            var hcs = new EndpointHealthCheck();
            hcs.EndpointLabel = Environment.GetEnvironmentVariable("EndpointLabel");
            hcs.Endpoint = Environment.GetEnvironmentVariable("Endpoint"); // A B or C
            hcs.StatusCode = healthy ? "200" : "503";
            hcs.Ticks = DateTime.UtcNow.Ticks;
            hcs.Style = healthy ? "success" : "failed";

            try
            {
                await signalRMessages.AddAsync(
                    new SignalRMessage
                    {
                        Target = "ReceiveEndpointHealthCheckData",
                        Arguments = new[] { hcs }
                    });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (healthy)
            {
                return new OkResult();
            }
            else
            {
                return new StatusCodeResult(503);
            }
        }

       

        [FunctionName("toggleHealthy")]
        public static Task<IActionResult> ToggleHealthy(
          [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] object body)
        {
            IsHealthy = !IsHealthy;

            return Task.FromResult(new OkObjectResult(IsHealthy) as IActionResult);
        }


        [FunctionName("traffic")]
        public static async Task<IActionResult> Traffic(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] TrafficRaw message,
            [SignalR(HubName = "simulator")] IAsyncCollector<SignalRMessage> signalRMessages)
        {

            var ti = new TrafficInfo();
            ti.EndpointLabel = Environment.GetEnvironmentVariable("EndpointLabel");
            ti.Endpoint = Environment.GetEnvironmentVariable("Endpoint"); // A B or C
            ti.Source = message.Source;
            ti.Style = message.Style;

            ti.Ticks = DateTime.UtcNow.Ticks;

            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "ReceiveTrafficData",
                    Arguments = new[] { ti }
                });

            return new OkResult();
        }
    }
}
