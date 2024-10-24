using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

public interface IEvaluatorFactory<in TEvaluated>
{
    /// <exception cref="ArgumentException">When the configuration is invalid for the output data type</exception>
    IEvaluator<TEvaluated> CreateEvaluator(ConditionConfiguration configuration);
}