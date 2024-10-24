using RealTimeWeatherMonitoringApp.Domain.Interfaces.Repository;
using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Repository;

public class BotRepository<TEvaluated> : IBotRepository<TEvaluated>
{
    private readonly IBotInitializer<TEvaluated> _botInitializer;
    public BotRepository(IBotInitializer<TEvaluated> botInitializer) => _botInitializer = botInitializer;

    public IEnumerable<Bot<TEvaluated>> GetAll() => _botInitializer.InitializeAndGetAllBots();
}