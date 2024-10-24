using System.Xml.Linq;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Parsers;

public class WeatherDataXmlParser : IParsingStrategy<WeatherData>
{
    public bool TryParse(string input, out WeatherData? result)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(input)) return false;

        try
        {
            var doc = XDocument.Parse(input);

            var weatherDataElement = doc.Element("WeatherData");
            if (weatherDataElement == null) return false;

            var locationElement = weatherDataElement.Element("Location");
            var temperatureElement = weatherDataElement.Element("Temperature");
            var humidityElement = weatherDataElement.Element("Humidity");

            if (locationElement == null || temperatureElement == null || humidityElement == null)
                return false;

            result = new WeatherData(
                location: locationElement.Value,
                temperature: double.Parse(temperatureElement.Value),
                humidity: double.Parse(humidityElement.Value));
            return true;
        }
        catch (Exception)
        {
            result = null;
            return false;
        }
    }
}