using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Repository;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Models.Controller;

namespace RealTimeWeatherMonitoringApp.Domain.Service;

public class BotControllerService<TEvaluated> : IBotControllerService<TEvaluated>
{
    private readonly Lazy<IEnumerable<BotController<TEvaluated>>> _botControllers;

    public BotControllerService(IBotRepository<TEvaluated> repository)
    {
        _botControllers = new Lazy<IEnumerable<BotController<TEvaluated>>>(() =>
            repository.GetAll().Select(b => new BotController<TEvaluated>(b)));
    }

    public IEnumerable<IBotController<TEvaluated>> GetBotControllers() => _botControllers.Value;

    public IEnumerable<IBotController<TEvaluated>> GetBotControllers(IBotPublishingService publishingService)
    {
        return _botControllers.Value.Select(botController =>
            new PublisherBotController<TEvaluated>(botController, publishingService));
    }
}