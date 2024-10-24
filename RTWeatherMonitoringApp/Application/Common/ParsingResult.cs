namespace RealTimeWeatherMonitoringApp.Application.Common;

public class ParsingResult<TResult> : Result
{
    public TResult? Data { get; set; }

    public ParsingResult(bool success, string message) : base(success, message)
    {
    }
}