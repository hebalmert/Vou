using Newtonsoft.Json;

namespace Vou.Common.CountryApi
{
    public class StateResponse
    {
        [JsonProperty("id")]
        public long StateId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("iso2")]
        public string? Iso2 { get; set; }
    }
}
