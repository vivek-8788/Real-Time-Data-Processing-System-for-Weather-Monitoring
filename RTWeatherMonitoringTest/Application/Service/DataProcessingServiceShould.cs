using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Application.Common;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Interfaces.Service;
using RealTimeWeatherMonitoringApp.Application.Service;
using RealTimeWeatherMonitoringTest.Common;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Application.Service;

public class DataProcessingServiceShould
{
    [Theory, AutoMoqData]
    public void Process_SuccessfulParsing_SendDataToReceiver(
        string goodInput,
        ParsingResult<TestData> parsingResult,
        [Frozen] Mock<IAutoParsingService<TestData>> autoParsingServiceMock,
        [Frozen] Mock<IDataReceiver<TestData>> dataReceiverMock,
        DataProcessingService<TestData> dataProcessingService)
    {
        parsingResult.Success = true;
        autoParsingServiceMock
            .Setup(s => s.AutoParse(goodInput))
            .Returns(parsingResult);

        var result = dataProcessingService.Process(goodInput);

        dataReceiverMock.Verify(r => r.Receive(parsingResult.Data), Times.Once);
        result.Should().BeSameAs(parsingResult);
    }

    [Theory, AutoMoqData]
    public async Task ProcessAsync_SuccessfulAsyncParsing_SendDataToReceiverAsync(
        string goodInput,
        ParsingResult<TestData> parsingResult,
        [Frozen] Mock<IAutoParsingService<TestData>> autoParsingServiceMock,
        [Frozen] Mock<IDataReceiver<TestData>> dataReceiverMock,
        DataProcessingService<TestData> dataProcessingService)
    {
        parsingResult.Success = true;
        autoParsingServiceMock
            .Setup(s => s.AutoParseAsync(goodInput))
            .ReturnsAsync(parsingResult);

        var result = await dataProcessingService.ProcessAsync(goodInput);

        dataReceiverMock.Verify(r => r.ReceiveAsync(parsingResult.Data), Times.Once);
        result.Should().BeSameAs(parsingResult);
    }

    [Theory, AutoMoqData]
    public void Process_FailedParsing_NotSendToReceiver(
        string badInput,
        ParsingResult<TestData> parsingResult,
        [Frozen] Mock<IAutoParsingService<TestData>> autoParsingServiceMock,
        [Frozen] Mock<IDataReceiver<TestData>> dataReceiverMock,
        DataProcessingService<TestData> dataProcessingService)
    {
        parsingResult.Success = false;
        autoParsingServiceMock
            .Setup(s => s.AutoParse(badInput))
            .Returns(parsingResult);

        var result = dataProcessingService.Process(badInput);

        dataReceiverMock.Verify(r => r.Receive(It.IsAny<TestData>()), Times.Never);
        result.Should().BeSameAs(parsingResult);
    }

    [Theory, AutoMoqData]
    public async Task ProcessAsync_FailedAsyncParsing_NotSendToReceiverAsync(
        string badInput,
        ParsingResult<TestData> parsingResult,
        [Frozen] Mock<IAutoParsingService<TestData>> autoParsingServiceMock,
        [Frozen] Mock<IDataReceiver<TestData>> dataReceiverMock,
        DataProcessingService<TestData> dataProcessingService)
    {
        parsingResult.Success = false;
        autoParsingServiceMock
            .Setup(s => s.AutoParseAsync(badInput))
            .ReturnsAsync(parsingResult);

        var result = await dataProcessingService.ProcessAsync(badInput);

        dataReceiverMock.Verify(r => r.ReceiveAsync(It.IsAny<TestData>()), Times.Never);
        result.Should().BeSameAs(parsingResult);
    }
}