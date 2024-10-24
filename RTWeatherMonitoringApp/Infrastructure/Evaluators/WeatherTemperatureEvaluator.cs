using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Evaluators;

public class WeatherTemperatureEvaluator : IEvaluator<WeatherData>
{
    private readonly double _threshold;
    private readonly Func<double, double, bool> _comparison;

    public WeatherTemperatureEvaluator(double threshold, Func<double, double, bool> comparison)
    {
        _threshold = threshold;
        _comparison = comparison;
    }

    public bool Evaluate(WeatherData data) => _comparison(data.Temperature, _threshold);
}