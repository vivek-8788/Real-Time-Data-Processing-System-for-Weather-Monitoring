namespace RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;

public interface IBotControllerService<TEvaluated>
{
    IEnumerable<IBotController<TEvaluated>> GetBotControllers();

    /// <summary>
    /// Attaches the bot controllers to the given publishing service before fetching them.
    /// </summary>
    /// <param name="publishingService">A publishing service that will receive all bot controllers events</param>
    /// <returns>A collection of bot controllers attached to the given publishing service</returns>
    IEnumerable<IBotController<TEvaluated>> GetBotControllers(IBotPublishingService publishingService);
}