using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dctx.TrafficGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpoint = System.Environment.GetEnvironmentVariable("Url");
            var sleep = System.Environment.GetEnvironmentVariable("SleepTime");
            var source = System.Environment.GetEnvironmentVariable("Source");
            var style = System.Environment.GetEnvironmentVariable("Style");

            HttpClient client = new HttpClient();

            Console.WriteLine("Started.");

            while (true)
            {
                var tr = new TrafficRaw()
                {
                    Source = source,
                    Style = style
                };

                try
                {
                    var data = JsonConvert.SerializeObject(tr);

                    Console.WriteLine($"Sending to {endpoint} - data {data}");
                    var r = await client.PostAsync(endpoint, new StringContent(data));

                    Console.WriteLine($"{data}");

                    Thread.Sleep(int.Parse(sleep));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public class TrafficRaw
        {
            public string Source { get; set; }
            public string Style { get; set; }
        }


    }
}
