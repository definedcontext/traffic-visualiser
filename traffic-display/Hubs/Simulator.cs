using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Dctx.TrafficDisplay.Hubs
{
    public class Simulator : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        public class EndpointHealthCheck
        {
            public string Source { get; set; }
            public string StatusCode { get; set; }
            public string Color { get; set; }
            public long Ticks { get; set; }
        }

        public async Task SendEndointStatuResult(EndpointHealthCheck data)
        {
            await Clients.All.SendAsync("ReceiveEndpointHealthCheckData", data);
        }
    }
}