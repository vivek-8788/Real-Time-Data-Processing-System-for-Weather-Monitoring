using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Service;

/// <summary>
/// Appends the base directory to the given relative path
/// </summary>
public class RelativeFileReader : IFileReader
{
    private readonly IFileReader _fileReader;
    public RelativeFileReader(IFileReader fileReader) => _fileReader = fileReader;

    public string ReadAllText(string path)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var fullPath = Path.Combine(baseDirectory, path);
        return _fileReader.ReadAllText(fullPath);
    }
}