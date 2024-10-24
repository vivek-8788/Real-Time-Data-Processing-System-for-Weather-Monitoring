using RealTimeWeatherMonitoringApp.Domain.Interfaces;

namespace RealTimeWeatherMonitoringApp.Infrastructure.Extension;

public static class EvaluatorExtension
{
    /// <summary>
    /// Composes multiple evaluators into a single evaluator that returns true only if all individual evaluators return true.
    /// </summary>
    /// <param name="evaluators">A collection of evaluators to be composed.</param>
    /// <returns>An evaluator that aggregates the evaluation results of all provided evaluators.</returns>
    public static IEvaluator<TEvaluated> EvaluateAll<TEvaluated>(this IEnumerable<IEvaluator<TEvaluated>> evaluators) =>
        new AllEvaluator<TEvaluated>(evaluators);

    private class AllEvaluator<TEvaluated> : IEvaluator<TEvaluated>
    {
        private readonly IEnumerable<IEvaluator<TEvaluated>> _evaluators;
        public AllEvaluator(IEnumerable<IEvaluator<TEvaluated>> evaluators) => _evaluators = evaluators;

        public bool Evaluate(TEvaluated data) => _evaluators.All(evaluator => evaluator.Evaluate(data));
    }
}