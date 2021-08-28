using System;
using System.ComponentModel.DataAnnotations;

namespace Weather.Data
{
    public class Weather
    {
        [Key] public Guid Id { get; set; }
        public int Humidity { get; set; }
        public int Temperature { get; set; }
        public int Min_Temperature { get; set; }
        public int Max_Temperature { get; set; }
    }
}