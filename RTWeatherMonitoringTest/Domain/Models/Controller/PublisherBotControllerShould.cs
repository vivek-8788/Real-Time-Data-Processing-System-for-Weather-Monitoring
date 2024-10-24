using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Domain.Common;
using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Models.Controller;
using RealTimeWeatherMonitoringTest.Common;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Domain.Models.Controller;

public class PublisherBotControllerShould
{
    [Theory, AutoMoqData]
    public void React_BotControllerIgnoresEvent_NotPublishAnything(
        DataChangeEventArgs<TestData> inputEventArgs,
        [Frozen] Mock<IBotController<TestData>> botControllerMock,
        [Frozen] Mock<IBotPublishingService> publishingServiceMock,
        PublisherBotController<TestData> publisherBotController)
    {
        botControllerMock
            .Setup(c => c.React(inputEventArgs))
            .Returns((BotEventArgs?)null);

        var eventArgs = publisherBotController.React(inputEventArgs);

        eventArgs.Should().BeNull();
        publishingServiceMock.Verify(s => s.Publish(It.IsAny<BotEventArgs>()), Times.Never);
    }

    [Theory, AutoMoqData]
    public void React_BotControllerCreatesEvent_PublishEvent(
        DataChangeEventArgs<TestData> inputEventArgs,
        BotEventArgs outputEventArgs,
        [Frozen] Mock<IBotController<TestData>> botControllerMock,
        [Frozen] Mock<IBotPublishingService> publishingServiceMock,
        PublisherBotController<TestData> publisherBotController)
    {
        botControllerMock
            .Setup(c => c.React(inputEventArgs))
            .Returns(outputEventArgs);

        var eventArgs = publisherBotController.React(inputEventArgs);

        eventArgs.Should().BeSameAs(outputEventArgs);
        publishingServiceMock.Verify(s => s.Publish(It.IsAny<BotEventArgs>()), Times.Once);
    }
}