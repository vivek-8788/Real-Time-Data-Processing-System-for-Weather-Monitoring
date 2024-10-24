using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces.Factory;
using RealTimeWeatherMonitoringApp.Infrastructure.Service;
using RealTimeWeatherMonitoringTest.Common;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Infrastructure.Service;

public class BotInitializerShould
{
    [Theory, AutoMoqData]
    public void InitializeAndGetAllBots_NoInitializationAtConstruction(
        [Frozen] Mock<IConfigurationFactory> configurationFactoryMock,
        [Frozen] Mock<IBotFactory<TestData>> botFactoryMock,
        BotInitializer<TestData> botInitializer)
    {
        botInitializer.Should().NotBeNull();
        configurationFactoryMock.VerifyNoOtherCalls();
        botFactoryMock.VerifyNoOtherCalls();
    }

    [Theory, AutoMoqData]
    public void InitializeAndGetAllBots_ReturnAllBots(
        List<(BotConfiguration configuration, Bot<TestData> bot)> configurationBotPairs,
        [Frozen] Mock<IConfigurationFactory> configurationFactoryMock,
        [Frozen] Mock<IBotFactory<TestData>> botFactoryMock,
        BotInitializer<TestData> botInitializer)
    {
        configurationFactoryMock
            .Setup(f => f.CreateBotConfigurations())
            .Returns(configurationBotPairs.Select(cb => cb.configuration));

        foreach (var cb in configurationBotPairs)
            botFactoryMock
                .Setup(f => f.CreateBot(cb.configuration))
                .Returns(cb.bot);

        var bots = botInitializer.InitializeAndGetAllBots();

        bots.Should().BeEquivalentTo(configurationBotPairs.Select(cb => cb.bot));
        configurationFactoryMock.Verify(f => f.CreateBotConfigurations(), Times.Once);
        foreach (var cb in configurationBotPairs)
            botFactoryMock.Verify(f => f.CreateBot(cb.configuration), Times.Once);
    }
}