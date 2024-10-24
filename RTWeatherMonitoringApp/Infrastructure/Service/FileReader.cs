using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Service;

public class FileReader : IFileReader
{
    public string ReadAllText(string path) => File.ReadAllText(path);
}