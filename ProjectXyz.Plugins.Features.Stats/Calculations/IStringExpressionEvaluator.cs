using System;

namespace ProjectXyz.Plugins.Features.Stats.Calculations
{
    public interface IStringExpressionEvaluator : IDisposable
    {
        double Evaluate(string expression);
    }
}