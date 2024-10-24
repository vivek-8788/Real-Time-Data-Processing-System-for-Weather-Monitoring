using RealTimeWeatherMonitoringApp.Domain.Common;

namespace RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;

/// <summary>
/// Responsible for handling published bot events
/// </summary>
public interface IBotPublishingService
{
    void Publish(BotEventArgs e);
}