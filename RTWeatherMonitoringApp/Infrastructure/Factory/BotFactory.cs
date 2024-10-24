using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Extension;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Factory;

public class BotFactory<TEvaluated> : IBotFactory<TEvaluated>
{
    private readonly IEvaluatorFactory<TEvaluated> _evaluatorFactory;
    public BotFactory(IEvaluatorFactory<TEvaluated> evaluatorFactory) => _evaluatorFactory = evaluatorFactory;

    public Bot<TEvaluated> CreateBot(BotConfiguration configuration)
    {
        var evaluator = configuration.Conditions
            .Select(config => _evaluatorFactory.CreateEvaluator(config))
            .EvaluateAll();

        return new Bot<TEvaluated>(
            configuration.Name,
            configuration.Enabled,
            configuration.Message,
            evaluator);
    }
}