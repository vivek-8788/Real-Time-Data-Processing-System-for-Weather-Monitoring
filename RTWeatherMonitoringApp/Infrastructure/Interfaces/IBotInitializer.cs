using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;

/// <typeparam name="TEvaluated">Determines the type of bots to initialize, and what data they evaluate</typeparam>
public interface IBotInitializer<TEvaluated>
{
    IEnumerable<Bot<TEvaluated>> InitializeAndGetAllBots();
}