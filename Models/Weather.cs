using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.Models
{
    public class Weather
    {
        [Key] public Guid Id { get; set; }
        public int Humidity { get; set; }
        public int Temperature { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
    }
}