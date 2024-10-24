using RealTimeWeatherMonitoringApp.Domain.Models;

namespace RealTimeWeatherMonitoringApp.Domain.Interfaces.Repository;

public interface IBotRepository<TEvaluated>
{
    IEnumerable<Bot<TEvaluated>> GetAll();
}