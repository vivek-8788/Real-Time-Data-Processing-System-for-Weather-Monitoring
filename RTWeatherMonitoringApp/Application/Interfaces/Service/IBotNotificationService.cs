using RealTimeWeatherMonitoringApp.Domain.Common;

namespace RealTimeWeatherMonitoringApp.Application.Interfaces.Service;

/// <summary>
/// Allows subscription to bot notifications
/// </summary>
public interface IBotNotificationService
{
    event EventHandler<BotEventArgs> OnBotNotification;
}