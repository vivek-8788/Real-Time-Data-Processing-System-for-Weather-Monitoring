using System.Text.Json;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Parsers;

public class WeatherDataJsonParser : IParsingStrategy<WeatherData>
{
    public bool TryParse(string input, out WeatherData? result)
    {
        try
        {
            result = JsonSerializer.Deserialize<WeatherData>(input);
            return result != null;
        }
        catch (JsonException)
        {
            result = null;
            return false;
        }
    }
}