using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration.Condition;
using RealTimeWeatherMonitoringApp.Infrastructure.Evaluators;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Factory;

public class WeatherEvaluatorFactory : IEvaluatorFactory<WeatherData>
{
    public IEvaluator<WeatherData> CreateEvaluator(ConditionConfiguration config)
    {
        var value = config.Value;
        var comparison = GetComparisonOperator(config.Operator);
        return config.Type switch
        {
            ConditionType.Temperature => new WeatherTemperatureEvaluator(value, comparison),
            ConditionType.Humidity => new WeatherHumidityEvaluator(value, comparison),
            _ => throw new ArgumentException($"Unsupported weather condition type: {config.Type}")
        };
    }

    private Func<double, double, bool> GetComparisonOperator(ConditionOperator @operator)
    {
        return @operator switch
        {
            ConditionOperator.GreaterThan => (x, y) => x > y,
            ConditionOperator.LessThan => (x, y) => x < y,
            _ => throw new ArgumentException($"Unsupported weather condition operator: {@operator}")
        };
    }
}