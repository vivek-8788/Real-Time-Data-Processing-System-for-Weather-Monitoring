using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Application.Service;
using RealTimeWeatherMonitoringApp.Domain.Common;
using RealTimeWeatherMonitoringTest.Common;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Application.Service;

public class MonitoringServiceShould
{
    [Theory, AutoMoqData]
    public void Receive_Always_InvokeSubscribers(
        TestData data,
        Mock<EventHandler<DataChangeEventArgs<TestData>>> mockEventHandler,
        MonitoringService<TestData> monitoringService)
    {
        monitoringService.OnDataChange += mockEventHandler.Object;

        monitoringService.Receive(data);

        mockEventHandler.Verify(
            h => h.Invoke(It.IsAny<object>(), It.IsAny<DataChangeEventArgs<TestData>>()),
            Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task ReceiveAsync_Always_InvokeSubscribers(
        TestData data,
        Mock<EventHandler<DataChangeEventArgs<TestData>>> mockEventHandler,
        MonitoringService<TestData> monitoringService)
    {
        monitoringService.OnDataChange += mockEventHandler.Object;

        await monitoringService.ReceiveAsync(data);

        mockEventHandler.Verify(
            h => h.Invoke(It.IsAny<object>(), It.IsAny<DataChangeEventArgs<TestData>>()),
            Times.Once);
    }

    [Theory, AutoData]
    public void Receive_Always_SendSubscribersTheSameData(
        TestData data,
        MonitoringService<TestData> monitoringService)
    {
        DataChangeEventArgs<TestData>? capturedEventArgs = null;
        monitoringService.OnDataChange += (_, args) => capturedEventArgs = args;

        monitoringService.Receive(data);

        capturedEventArgs.Should().NotBeNull();
        capturedEventArgs.NewData.Should().BeSameAs(data);
    }

    [Theory, AutoData]
    public async Task ReceiveAsync_Always_SendSubscribersTheSameData(
        TestData data,
        MonitoringService<TestData> monitoringService)
    {
        DataChangeEventArgs<TestData>? capturedEventArgs = null;
        monitoringService.OnDataChange += (_, args) => capturedEventArgs = args;

        await monitoringService.ReceiveAsync(data);

        capturedEventArgs.Should().NotBeNull();
        capturedEventArgs.NewData.Should().BeSameAs(data);
    }
}