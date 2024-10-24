using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;

public interface IBotFactory<TEvaluated>
{
    Bot<TEvaluated> CreateBot(BotConfiguration configuration);
}