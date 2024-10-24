using RealTimeWeatherMonitoringApp.Domain.Common;

namespace RealTimeWeatherMonitoringApp.Application.Interfaces;

/// <summary>
/// Notifies subscribers about changes in data of type <typeparamref name="TObserved"/>.
/// </summary>
/// <typeparam name="TObserved">The observed data type.</typeparam>
public interface IDataChangeNotifier<TObserved>
{
    event EventHandler<DataChangeEventArgs<TObserved>> OnDataChange;
}