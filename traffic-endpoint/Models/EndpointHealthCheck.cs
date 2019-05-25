using System;
namespace Dctx.TrafficEndpoint.Models
{
    public class EndpointHealthCheck
    {
        public string EndpointLabel { get; set; }
        public string Endpoint { get; set; }
        public string StatusCode { get; set; }
        public string Style { get; set; }
        public long Ticks { get; set; }
    }
}
