using Newtonsoft.Json;

namespace Vou.Shared.CountryApi
{
    public class CityResponse
    {
        [JsonProperty("id")]
        public long CityId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
