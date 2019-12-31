using System;

namespace ProjectXyz.Api.Stats.Calculations
{
    public interface IStringExpressionEvaluator : IDisposable
    {
        double Evaluate(string expression);
    }
}