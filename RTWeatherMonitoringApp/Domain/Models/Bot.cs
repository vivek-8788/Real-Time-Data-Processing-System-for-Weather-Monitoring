using RealTimeWeatherMonitoringApp.Domain.Interfaces;

namespace RealTimeWeatherMonitoringApp.Domain.Models;

/// <summary>
/// Bot information and its evaluator
/// </summary>
/// <typeparam name="TEvaluated">Evaluated data type</typeparam>
public class Bot<TEvaluated>
{
    public string Name { get; }
    public bool Enabled { get; }
    public string Message { get; }
    public IEvaluator<TEvaluated> Evaluator { get; }

    public Bot(string name, bool enabled, string message, IEvaluator<TEvaluated> evaluator)
    {
        Name = name;
        Enabled = enabled;
        Message = message;
        Evaluator = evaluator;
    }
}