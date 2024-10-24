namespace RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;

public interface IFileReader
{
    string ReadAllText(string path);
}