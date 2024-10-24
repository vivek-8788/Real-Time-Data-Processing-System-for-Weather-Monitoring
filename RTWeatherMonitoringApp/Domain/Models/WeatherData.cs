namespace RealTimeWeatherMonitoringApp.Domain.Models;

public class WeatherData
{
    public string Location { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }

    public WeatherData(string location, double temperature, double humidity)
    {
        Location = location;
        Temperature = temperature;
        Humidity = humidity;
    }

    public override string ToString() =>
        $"WeatherData[Location={Location}, Temperature={Temperature}, Humidity={Humidity}]";
}