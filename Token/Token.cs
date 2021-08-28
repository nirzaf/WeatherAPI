using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.Token
{
    public class Token
    {
        [Key] public Guid TokenValue { get; set; }
    }
}