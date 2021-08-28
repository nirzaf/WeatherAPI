using Newtonsoft.Json;

namespace WeatherAPI.Models
{
    public class WeatherViewModel
    {
        [JsonProperty("humidity")] public long Humidity { get; set; }

        [JsonProperty("temperature")] public long Temperature { get; set; }

        [JsonProperty("min_temperature")] public long MinTemperature { get; set; }

        [JsonProperty("max_temperature")] public long MaxTemperature { get; set; }
    }
}