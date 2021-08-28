using Newtonsoft.Json;

namespace WeatherAPI.Models
{
    public class WeatherViewModel
    {
        [JsonProperty("humidity")] public int Humidity { get; set; }

        [JsonProperty("temperature")] public int Temperature { get; set; }

        [JsonProperty("min_temperature")] public int MinTemperature { get; set; }

        [JsonProperty("max_temperature")] public int MaxTemperature { get; set; }
    }
}