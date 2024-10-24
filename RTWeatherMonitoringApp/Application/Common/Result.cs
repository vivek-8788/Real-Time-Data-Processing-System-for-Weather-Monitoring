namespace RealTimeWeatherMonitoringApp.Application.Common;

public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public bool Fail => !Success;

    public Result(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}