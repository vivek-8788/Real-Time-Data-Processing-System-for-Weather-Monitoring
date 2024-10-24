namespace RealTimeWeatherMonitoringApp.Infrastructure.Configuration;

public record BotConfiguration(
    string Name,
    bool Enabled,
    string Message,
    IEnumerable<ConditionConfiguration> Conditions);