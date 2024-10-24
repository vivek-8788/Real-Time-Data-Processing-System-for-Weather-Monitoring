using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;

namespace RealTimeWeatherMonitoringApp.Application.Service;

public class BotEventManager<TData> : IBotEventManager<TData>
{
    private readonly IBotControllerService<TData> _botControllerService;

    public BotEventManager(IBotControllerService<TData> botControllerService) =>
        _botControllerService = botControllerService;

    public void Attach(IDataChangeNotifier<TData> notifier, IBotPublishingService publisher)
    {
        var botControllers = _botControllerService.GetBotControllers(publisher);
        foreach (var controller in botControllers)
            notifier.OnDataChange += (_, args) => controller.React(args);
    }
}