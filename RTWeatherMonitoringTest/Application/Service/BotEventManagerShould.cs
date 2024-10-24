using AutoFixture.Xunit2;
using Moq;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Service;
using RealTimeWeatherMonitoringApp.Domain.Common;
using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Domain.Interfaces.Service;
using RealTimeWeatherMonitoringTest.Common;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Application.Service;

public class BotEventManagerShould
{
    [Theory, AutoMoqData]
    public void Attach_WhenDataChanges_BotControllersReactAndPublishToServices(
        DataChangeEventArgs<TestData> dataChangeEventArgs,
        BotEventArgs botEventArgs,
        List<Mock<IBotController<TestData>>> botControllerMocks,
        Mock<IBotPublishingService> botPublishingServiceMock,
        Mock<IDataChangeNotifier<TestData>> dataChangeNotifierMock,
        [Frozen] Mock<IBotControllerService<TestData>> botControllerServiceMock,
        BotEventManager<TestData> botEventManager)
    {
        foreach (var botControllerMock in botControllerMocks)
            botControllerMock
                .Setup(c => c.React(dataChangeEventArgs))
                .Callback(() => botPublishingServiceMock.Object.Publish(botEventArgs))
                .Returns(botEventArgs);

        botControllerServiceMock
            .Setup(s => s.GetBotControllers(botPublishingServiceMock.Object))
            .Returns(botControllerMocks.Select(m => m.Object));

        botEventManager.Attach(dataChangeNotifierMock.Object, botPublishingServiceMock.Object);
        dataChangeNotifierMock.Raise(n => n.OnDataChange += null, dataChangeEventArgs);

        foreach (var botControllerMock in botControllerMocks)
            botControllerMock.Verify(c => c.React(dataChangeEventArgs), Times.Once);

        botPublishingServiceMock.Verify(s => s.Publish(botEventArgs),
            Times.Exactly(botControllerMocks.Count));
    }
}