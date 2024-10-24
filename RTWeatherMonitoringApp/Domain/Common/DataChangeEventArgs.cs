namespace RealTimeWeatherMonitoringApp.Domain.Common;

public class DataChangeEventArgs<TObserved> : EventArgs
{
    public TObserved? NewData { get; }
    public DataChangeEventArgs(TObserved? newData) => NewData = newData;
}