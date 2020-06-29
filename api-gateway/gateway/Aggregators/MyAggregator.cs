using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;

namespace gateway.Aggregators
{
    public class MyAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<DownstreamContext> responses)
        {
            var responseStrings = responses.Select(response => {
                return response.DownstreamResponse.Content.ReadAsStringAsync();
            }).ToList();
            var resp = new StringContent($"{{\"command\": {await responseStrings[0]}, \"price\": {await responseStrings[1]}}}");
            var dst = new DownstreamResponse(resp, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
            return dst;
        }
    }
}