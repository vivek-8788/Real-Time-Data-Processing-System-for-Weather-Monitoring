using RealTimeWeatherMonitoringApp.Domain.Common;

namespace RealTimeWeatherMonitoringApp.Domain.Interfaces;

/// <summary>
/// Controls the behaviour of a Bot model
/// </summary>
/// <typeparam name="TEvaluated">Evaluated data type of the bot</typeparam>
public interface IBotController<TEvaluated>
{
    /// <summary>
    /// Reacts to the incoming <see cref="DataChangeEventArgs{TEvaluated}"/>
    /// and potentially returns an event args if the data triggers a response.
    /// </summary>
    /// <param name="args">The event args containing the data to be evaluated by the bot controller.</param>
    /// <returns>A <see cref="BotEventArgs"/> if the data triggers the bot, otherwise null.</returns>
    BotEventArgs? React(DataChangeEventArgs<TEvaluated> args);
}