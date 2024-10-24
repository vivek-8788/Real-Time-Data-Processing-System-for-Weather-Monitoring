using RealTimeWeatherMonitoringApp.Infrastructure.Configuration.Condition;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Configuration;

public record ConditionConfiguration(
    ConditionType Type,
    ConditionOperator Operator,
    double Value);