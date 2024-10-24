using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

public interface IConfigurationFactory
{
    IEnumerable<BotConfiguration> CreateBotConfigurations();
}