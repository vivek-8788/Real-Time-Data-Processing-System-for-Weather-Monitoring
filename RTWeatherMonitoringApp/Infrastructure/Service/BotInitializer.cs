using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Service;

public class BotInitializer<TEvaluated> : IBotInitializer<TEvaluated>
{
    private readonly Lazy<IEnumerable<Bot<TEvaluated>>> _bots;

    public BotInitializer(IConfigurationFactory configurationFactory, IBotFactory<TEvaluated> botFactory)
    {
        _bots = new Lazy<IEnumerable<Bot<TEvaluated>>>(() => configurationFactory
            .CreateBotConfigurations()
            .Select(botFactory.CreateBot));
    }

    public IEnumerable<Bot<TEvaluated>> InitializeAndGetAllBots() => _bots.Value;
}