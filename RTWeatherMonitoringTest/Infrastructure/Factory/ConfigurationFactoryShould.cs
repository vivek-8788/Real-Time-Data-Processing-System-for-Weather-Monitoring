using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration;
using RealTimeWeatherMonitoringApp.Infrastructure.Configuration.Condition;
using RealTimeWeatherMonitoringApp.Infrastructure.Factory;
using RealTimeWeatherMonitoringApp.Infrastructure.Interfaces;
using RealTimeWeatherMonitoringTest.Common;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Infrastructure.Factory;

public class ConfigurationFactoryShould
{
    private static string GetTestJsonConfiguration() =>
        """
        {
            "TestBot": {
                "enabled": true,
                "conditions": [
                    {
                        "type": "humidity",
                        "operator": "greaterThan",
                        "value": 50
                    }
                ],
                "message": "Bot Message"
            }
        }
        """;

    private static BotConfiguration GetTestBotConfiguration() => new(
        Name: "TestBot",
        Enabled: true,
        Message: "Bot Message",
        Conditions:
        [
            new ConditionConfiguration(
                Type: ConditionType.Humidity,
                Operator: ConditionOperator.GreaterThan,
                Value: 50)
        ]);

    [Theory, AutoMoqData]
    public void CreateBotConfigurations_NoFileReadingOnConstruction(
        [Frozen] Mock<IFileReader> fileReaderMock,
        ConfigurationFactory configurationFactory)
    {
        configurationFactory.Should().NotBeNull();
        fileReaderMock.VerifyNoOtherCalls();
    }

    [Theory, AutoMoqData]
    public void CreateBotConfigurations_WhenInvoked_ReturnConfigurations(
        string configurationFilepath,
        Mock<IFileReader> fileReaderMock)
    {
        fileReaderMock
            .Setup(r => r.ReadAllText(configurationFilepath))
            .Returns(GetTestJsonConfiguration());

        var configurationFactory = new ConfigurationFactory(configurationFilepath, fileReaderMock.Object);
        var configurations = configurationFactory.CreateBotConfigurations().ToList();

        fileReaderMock.Verify(r => r.ReadAllText(configurationFilepath), Times.Once);
        configurations.Should().ContainSingle();
        configurations.First().Should().BeEquivalentTo(GetTestBotConfiguration());
    }
}