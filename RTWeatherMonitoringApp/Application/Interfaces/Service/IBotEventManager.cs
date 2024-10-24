using RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;

namespace RealTimeWeatherMonitoringApp.Application.Interfaces.Service;

/// <summary>
/// Responsible for attaching bots to events (e.g. publishers, notifiers)
/// </summary>
/// <typeparam name="TData">Data type that bots deal with</typeparam>
public interface IBotEventManager<TData>
{
    /// <summary>
    /// Sets up the event flow for bots by attaching them to a data change notifier and configuring them to publish their events
    /// through a specified publishing service. Bots will react to data changes notified by <paramref name="notifier"/> and
    /// their responses, if any, will be published to <paramref name="publisher"/>.
    /// </summary>
    /// <param name="notifier">Bots will subscribe to this notifier to receive updates on data changes relevant to their operations.</param>
    /// <param name="publisher">The publishing service through which bots can publish their events.</param>
    void Attach(IDataChangeNotifier<TData> notifier, IBotPublishingService publisher);
}