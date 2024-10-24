namespace RealTimeWeatherMonitoringApp.Domain.Interfaces;

/// <summary>
/// Defines the ability to evaluate certain data
/// </summary>
/// <typeparam name="TEvaluated">Evaluated data type</typeparam>
public interface IEvaluator<in TEvaluated>
{
    bool Evaluate(TEvaluated data);
}