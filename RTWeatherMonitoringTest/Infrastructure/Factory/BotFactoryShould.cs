using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Factory;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;
using RealTimeWeatherMonitoringTest.Common;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Infrastructure.Factory;

public class BotFactoryShould
{
    [Theory, AutoMoqData]
    public void CreateBot_MapConfigurationToBot(
        BotConfiguration configuration,
        Mock<IEvaluator<TestData>> evaluatorStub,
        [Frozen] Mock<IEvaluatorFactory<TestData>> evaluatorFactoryMock,
        BotFactory<TestData> botFactory)
    {
        evaluatorFactoryMock
            .Setup(f => f.CreateEvaluator(It.IsAny<ConditionConfiguration>()))
            .Returns(evaluatorStub.Object);

        var bot = botFactory.CreateBot(configuration);

        bot.Should().NotBeNull()
            .And.Match<Bot<TestData>>(b =>
                b.Name == configuration.Name &&
                b.Enabled == configuration.Enabled &&
                b.Message == configuration.Message);
    }

    [Theory, AutoMoqData]
    public void CreateBot_ComposeAllConditions(
        BotConfiguration configuration,
        Mock<IEvaluator<TestData>> evaluatorMock,
        TestData data,
        [Frozen] Mock<IEvaluatorFactory<TestData>> evaluatorFactoryMock,
        BotFactory<TestData> botFactory)
    {
        evaluatorMock.Setup(e => e.Evaluate(data)).Returns(true);
        evaluatorFactoryMock
            .Setup(f => f.CreateEvaluator(It.IsAny<ConditionConfiguration>()))
            .Returns(evaluatorMock.Object);

        var bot = botFactory.CreateBot(configuration);
        var result = bot.Evaluator.Evaluate(data);

        result.Should().BeTrue();
        evaluatorMock.Verify(e => e.Evaluate(data),
            Times.Exactly(configuration.Conditions.Count()));
    }
}