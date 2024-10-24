using FluentAssertions;
using Moq;
using RealTimeWeatherMonitoringApp.Domain.Interfaces;
using RealTimeWeatherMonitoringApp.Infrastructure.Extension;
using RealTimeWeatherMonitoringTest.Common;
using RealTimeWeatherMonitoringTest.Domain.Models;
using Xunit;

namespace RealTimeWeatherMonitoringTest.Infrastructure.Extension;

public class EvaluatorExtensionShould
{
    [Theory, AutoMoqData]
    public void EvaluateAll_FirstEvaluatorIsFalse_EarlyReturnFalse(
        TestData data,
        Mock<IEvaluator<TestData>> falseEvaluatorMock,
        Mock<IEvaluator<TestData>> trueEvaluatorMock)
    {
        falseEvaluatorMock.Setup(e => e.Evaluate(data)).Returns(false);
        trueEvaluatorMock.Setup(e => e.Evaluate(data)).Returns(true);
        var list = new[] { falseEvaluatorMock.Object, trueEvaluatorMock.Object };

        var composedEvaluator = list.EvaluateAll();
        var result = composedEvaluator.Evaluate(data);

        result.Should().BeFalse();
        falseEvaluatorMock.Verify(e => e.Evaluate(data), Times.Once);
        trueEvaluatorMock.Verify(e => e.Evaluate(data), Times.Never);
    }

    [Theory, AutoMoqData]
    public void EvaluateAll_AllEvaluatorsAreTrue_ReturnTrue(
        TestData data,
        List<Mock<IEvaluator<TestData>>> trueEvaluatorMocks)
    {
        foreach (var evaluatorMock in trueEvaluatorMocks)
            evaluatorMock
                .Setup(e => e.Evaluate(data))
                .Returns(true);

        var composedEvaluator = trueEvaluatorMocks.Select(m => m.Object).EvaluateAll();
        var result = composedEvaluator.Evaluate(data);

        result.Should().BeTrue();
        foreach (var evaluatorMock in trueEvaluatorMocks)
            evaluatorMock.Verify(e => e.Evaluate(data), Times.Once);
    }
}