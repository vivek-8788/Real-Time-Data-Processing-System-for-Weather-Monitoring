using AutoFixture;
using FluentAssertions;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration.Condition;
using RealTimeWeatherMonitoringApp.Infrastructure.Evaluators;
using RealTimeWeatherMonitoringApp.Infrastructure.Factory;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Infrastructure.Factory;

public class WeatherEvaluatorFactoryShould
{
    private readonly Fixture _fixture = new();
    private readonly WeatherEvaluatorFactory _weatherEvaluatorFactory = new();

    [Fact]
    public void CreateEvaluator_WithTemperatureCondition_CreateWeatherTemperatureEvaluator()
    {
        _fixture.Inject(ConditionType.Temperature);
        var configuration = _fixture.Create<ConditionConfiguration>();

        var evaluator = _weatherEvaluatorFactory.CreateEvaluator(configuration);

        evaluator.Should().BeOfType<WeatherTemperatureEvaluator>();
    }

    [Fact]
    public void CreateEvaluator_WithHumidityCondition_CreateWeatherHumidityEvaluator()
    {
        _fixture.Inject(ConditionType.Humidity);
        var configuration = _fixture.Create<ConditionConfiguration>();

        var evaluator = _weatherEvaluatorFactory.CreateEvaluator(configuration);

        evaluator.Should().BeOfType<WeatherHumidityEvaluator>();
    }

    [Fact]
    public void CreateEvaluator_WithUnknownCondition_ThrowArgumentException()
    {
        _fixture.Inject((ConditionType)999);
        var configuration = _fixture.Create<ConditionConfiguration>();

        var evaluation = () => _weatherEvaluatorFactory.CreateEvaluator(configuration);

        evaluation.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateEvaluator_WithUnknownOperator_ThrowArgumentException()
    {
        _fixture.Inject((ConditionOperator)999);
        var configuration = _fixture.Create<ConditionConfiguration>();

        var evaluation = () => _weatherEvaluatorFactory.CreateEvaluator(configuration);

        evaluation.Should().Throw<ArgumentException>();
    }
}