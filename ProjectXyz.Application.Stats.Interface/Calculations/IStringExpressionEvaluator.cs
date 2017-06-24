using System;

namespace ProjectXyz.Application.Stats.Interface.Calculations
{
    public interface IStringExpressionEvaluator : IDisposable
    {
        double Evaluate(string expression);
    }
}