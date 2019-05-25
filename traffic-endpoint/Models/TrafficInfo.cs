using System;
namespace Dctx.TrafficEndpoint.Models
{

    public class TrafficInfo
    {
        public string EndpointLabel { get; set; }
        public string Endpoint { get; set; }
        public string Source { get; set; }
        public string Style { get; set; }
        public long Ticks { get; set; }
    }
}
