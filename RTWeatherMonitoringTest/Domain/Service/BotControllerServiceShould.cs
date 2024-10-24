using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Domain.Common;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Repository;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Domain.Models;
using RealTimeWeatherMonitoringApp.Domain.Models.Controller;
using RealTimeWeatherMonitoringApp.Domain.Service;
using RealTimeWeatherMonitoringTest.Common;
using RealTimeWeatherMonitoringTest.Domain.Common;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Domain.Service;

public class BotControllerServiceShould
{
    [Theory, AutoMoqData]
    public void GetBotControllers_Default_ReturnControllersLazily(
        List<Bot<TestData>> bots,
        [Frozen] Mock<IBotRepository<TestData>> repositoryMock,
        BotControllerService<TestData> service)
    {
        repositoryMock.Setup(r => r.GetAll()).Returns(bots);

        repositoryMock.Verify(r => r.GetAll(), Times.Never);
        var botControllers = service.GetBotControllers();
        repositoryMock.Verify(r => r.GetAll(), Times.Once);

        botControllers.Should().AllBeOfType<BotController<TestData>>()
            .And.HaveCount(bots.Count)
            .And.OnlyHaveUniqueItems();
    }

    [Theory, AutoMoqData]
    public void GetBotControllers_WithPublishingService_ReturnPublisherControllersLazily(
        List<Bot<TestData>> bots,
        Mock<IBotPublishingService> publishingServiceMock,
        [Frozen] Mock<IBotRepository<TestData>> repositoryMock,
        BotControllerService<TestData> service)
    {
        repositoryMock.Setup(r => r.GetAll()).Returns(bots);

        repositoryMock.Verify(r => r.GetAll(), Times.Never);
        var botControllers = service.GetBotControllers(publishingServiceMock.Object);
        repositoryMock.Verify(r => r.GetAll(), Times.Once);

        botControllers.Should().AllBeOfType<PublisherBotController<TestData>>()
            .And.HaveCount(bots.Count)
            .And.OnlyHaveUniqueItems();
    }

    [Theory, CustomAutoData(
         typeof(AutoMoqCustomization),
         typeof(ReactiveBotControllerCustomization<TestData>))]
    public void GetBotControllers_WithPublishingService_ControllersEventsPublishedOnService(
        List<Bot<TestData>> bots,
        Mock<IBotPublishingService> publishingServiceMock,
        DataChangeEventArgs<TestData> dataChangeEventArgs,
        [Frozen] Mock<IBotRepository<TestData>> repositoryMock,
        BotControllerService<TestData> service)
    {
        repositoryMock.Setup(r => r.GetAll()).Returns(bots);
        var botControllers = service.GetBotControllers(publishingServiceMock.Object);

        foreach (var controller in botControllers)
        {
            var eventArgs = controller.React(dataChangeEventArgs);
            publishingServiceMock.Verify(s => s.Publish(eventArgs), Times.Once);
        }
    }
}