namespace RealTimeWeatherMonitoringApp.Application.Interfaces;

/// <summary>
/// Defines the ability to receive data of type <typeparamref name="TReceived"/>.
/// </summary>
/// <typeparam name="TReceived">The received data type.</typeparam>
public interface IDataReceiver<in TReceived>
{
    void Receive(TReceived? data);

    Task ReceiveAsync(TReceived? data);
}