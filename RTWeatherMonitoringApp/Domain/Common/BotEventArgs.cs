namespace RealTimeWeatherMonitoringApp.Domain.Common;

public class BotEventArgs : EventArgs
{
    public string BotName { get; }
    public string Message { get; }

    public BotEventArgs(string botName, string message)
    {
        BotName = botName;
        Message = message;
    }
}