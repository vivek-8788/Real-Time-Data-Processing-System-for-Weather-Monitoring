using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;
using RealTimeWeatherMonitoringApp.Infrastructure.Repository;
using RealTimeWeatherMonitoringTest.Common;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Infrastructure.Repository;

public class BotRepositoryShould
{
    [Theory, AutoMoqData]
    public void GetAll_GetAllBotsFromBotInitializer(
        List<Bot<TestData>> initializedBots,
        [Frozen] Mock<IBotInitializer<TestData>> botInitializerMock,
        BotRepository<TestData> botRepository)
    {
        botInitializerMock
            .Setup(i => i.InitializeAndGetAllBots())
            .Returns(initializedBots);

        var bots = botRepository.GetAll();

        bots.Should().BeEquivalentTo(initializedBots);
    }
}