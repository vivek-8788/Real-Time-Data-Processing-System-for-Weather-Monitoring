using AutoFixture;
using Moq;
using RealTimeWeatherMonitoringApp.Domain.Interfaces;

namespace RealTimeWeatherMonitoringTest.Domain.Common;

/// <summary>
/// Ensures that generated evaluators always return fixed values (true/false).
/// Useful for testing evaluation logic for a bot.
/// </summary>
public class FixedEvaluatorCustomization<TEvaluated> : ICustomization
{
    private readonly bool _returnValue;
    public FixedEvaluatorCustomization(bool returnValue) => _returnValue = returnValue;

    public void Customize(IFixture fixture)
    {
        var evaluatorMock = new Mock<IEvaluator<TEvaluated>>();
        evaluatorMock
            .Setup(e => e.Evaluate(It.IsAny<TEvaluated>()))
            .Returns(_returnValue);
        fixture.Inject(evaluatorMock.Object);
    }
}