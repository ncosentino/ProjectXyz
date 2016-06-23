using System;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IStringExpressionEvaluator : IDisposable
    {
        double Evaluate(string expression);
    }
}