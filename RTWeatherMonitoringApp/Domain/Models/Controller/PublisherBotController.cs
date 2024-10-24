using RealTimeWeatherMonitoringApp.Domain.Common;
using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;

namespace RealTimeWeatherMonitoringApp.Domain.Models.Controller;

/// <summary>
/// A BotController that publishes its events to an IBotPublishingService
/// </summary>
public class PublisherBotController<TEvaluated> : IBotController<TEvaluated>
{
    private readonly IBotController<TEvaluated> _botController;
    private readonly IBotPublishingService _publishingService;

    public PublisherBotController(
        IBotController<TEvaluated> botController,
        IBotPublishingService publishingService)
    {
        _botController = botController;
        _publishingService = publishingService;
    }

    public BotEventArgs? React(DataChangeEventArgs<TEvaluated> args)
    {
        var botEventArgs = _botController.React(args);
        if (botEventArgs != null)
            _publishingService.Publish(botEventArgs);
        return botEventArgs;
    }
}