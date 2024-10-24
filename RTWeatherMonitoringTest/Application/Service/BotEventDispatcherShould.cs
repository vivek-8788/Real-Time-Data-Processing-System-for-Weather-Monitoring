using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Application.Service;
using RealTimeWeatherMonitoringApp.Domain.Common;
using RealTimeWeatherMonitoringTest.Common;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Application.Service;

public class BotEventDispatcherShould
{
    [Theory, AutoMoqData]
    public void Publish_Always_NotifySubscribers(
        BotEventArgs eventArgs,
        Mock<EventHandler<BotEventArgs>> mockEventHandler,
        BotEventDispatcher botEventDispatcher)
    {
        botEventDispatcher.OnBotNotification += mockEventHandler.Object;

        botEventDispatcher.Publish(eventArgs);

        mockEventHandler.Verify(
            h => h.Invoke(It.IsAny<object>(), It.IsAny<BotEventArgs>()),
            Times.Once);
    }

    [Theory, AutoData]
    public void Publish_Always_SendSubscribersTheSameEventArgs(
        BotEventArgs eventArgs,
        BotEventDispatcher botEventDispatcher)
    {
        BotEventArgs? capturedEventArgs = null;
        botEventDispatcher.OnBotNotification += (_, args) => capturedEventArgs = args;

        botEventDispatcher.Publish(eventArgs);

        capturedEventArgs.Should().NotBeNull()
            .And.BeSameAs(eventArgs);
    }
}