using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Common;

namespace RealTimeWeatherMonitoringApp.Application.Service;

/// <summary>
/// A simple data receiver that notifies its subscribers about any new data it receives.
/// </summary>
/// <typeparam name="TData">Monitored data type</typeparam>
public class MonitoringService<TData> : IDataReceiver<TData>, IDataChangeNotifier<TData>
{
    public void Receive(TData? data) =>
        OnDataChange?.Invoke(this, new DataChangeEventArgs<TData>(data));

    public async Task ReceiveAsync(TData? data) => await Task.Run(() => Receive(data));

    public event EventHandler<DataChangeEventArgs<TData>>? OnDataChange;
}