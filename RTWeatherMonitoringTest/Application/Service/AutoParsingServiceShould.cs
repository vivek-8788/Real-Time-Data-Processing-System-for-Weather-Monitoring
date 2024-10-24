using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Application.Interfaces;
using RealTimeWeatherMonitoringApp.Application.Service;
using RealTimeWeatherMonitoringTest.Common;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Application.Service;

public class AutoParsingServiceShould
{
    [Theory, AutoMoqData]
    public void AutoParse_NoParsingStrategyWorks_ReturnFailResult(
        string input,
        List<Mock<IParsingStrategy<TestData>>> strategyMocks,
        AutoParsingService<TestData> autoParsingService)
    {
        foreach (var mock in strategyMocks)
        {
            autoParsingService.AddStrategy(mock.Object);
            mock.Setup(s => s.TryParse(input, out It.Ref<TestData?>.IsAny)).Returns(false);
        }

        var result = autoParsingService.AutoParse(input);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Data.Should().BeNull();
    }

    [Theory, AutoMoqData]
    public async Task AutoParseAsync_NoParsingStrategyWorks_ReturnFailResult(
        string input,
        List<Mock<IParsingStrategy<TestData>>> strategyMocks,
        AutoParsingService<TestData> autoParsingService)
    {
        foreach (var mock in strategyMocks)
        {
            autoParsingService.AddStrategy(mock.Object);
            mock.Setup(s => s.TryParse(input, out It.Ref<TestData?>.IsAny)).Returns(false);
        }

        var result = await autoParsingService.AutoParseAsync(input);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Data.Should().BeNull();
    }

    [Theory, AutoMoqData]
    public void AutoParse_AStrategyWorks_ReturnSuccessResult(
        string input,
        TestData? resultData,
        Mock<IParsingStrategy<TestData>> strategyMock,
        AutoParsingService<TestData> autoParsingService)
    {
        autoParsingService.AddStrategy(strategyMock.Object);
        strategyMock.Setup(s => s.TryParse(input, out resultData)).Returns(true);

        var result = autoParsingService.AutoParse(input);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().BeSameAs(resultData);
    }

    [Theory, AutoMoqData]
    public async Task AutoParseAsync_AStrategyWorks_ReturnSuccessResult(
        string input,
        TestData? resultData,
        Mock<IParsingStrategy<TestData>> strategyMock,
        AutoParsingService<TestData> autoParsingService)
    {
        autoParsingService.AddStrategy(strategyMock.Object);
        strategyMock.Setup(s => s.TryParse(input, out resultData)).Returns(true);

        var result = await autoParsingService.AutoParseAsync(input);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().BeSameAs(resultData);
    }

    [Theory, AutoMoqData]
    public void AutoParse_MultipleStrategiesWork_UseFirstOne(
        string input,
        TestData? firstResultData,
        TestData? secondResultData,
        Mock<IParsingStrategy<TestData>> firstStrategyMock,
        Mock<IParsingStrategy<TestData>> secondStrategyMock,
        AutoParsingService<TestData> autoParsingService)
    {
        autoParsingService.AddStrategy(firstStrategyMock.Object);
        autoParsingService.AddStrategy(secondStrategyMock.Object);

        firstStrategyMock.Setup(s => s.TryParse(input, out firstResultData)).Returns(true);
        secondStrategyMock.Setup(s => s.TryParse(input, out secondResultData)).Returns(true);

        autoParsingService.AutoParse(input);

        firstStrategyMock.Verify(s => s.TryParse(input, out firstResultData), Times.Once);
        secondStrategyMock.Verify(s => s.TryParse(input, out secondResultData), Times.Never);
    }

    [Theory, AutoMoqData]
    public async Task AutoParseAsync_MultipleStrategiesWork_UseFirstOne(
        string input,
        TestData? firstResultData,
        TestData? secondResultData,
        Mock<IParsingStrategy<TestData>> firstStrategyMock,
        Mock<IParsingStrategy<TestData>> secondStrategyMock,
        AutoParsingService<TestData> autoParsingService)
    {
        autoParsingService.AddStrategy(firstStrategyMock.Object);
        autoParsingService.AddStrategy(secondStrategyMock.Object);

        firstStrategyMock.Setup(s => s.TryParse(input, out firstResultData)).Returns(true);
        secondStrategyMock.Setup(s => s.TryParse(input, out secondResultData)).Returns(true);

        await autoParsingService.AutoParseAsync(input);

        firstStrategyMock.Verify(s => s.TryParse(input, out firstResultData), Times.Once);
        secondStrategyMock.Verify(s => s.TryParse(input, out secondResultData), Times.Never);
    }
}