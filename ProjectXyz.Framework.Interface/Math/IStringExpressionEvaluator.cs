using System;

namespace ProjectXyz.Framework.Interface.Math
{
    public interface IStringExpressionEvaluator : IDisposable
    {
        double Evaluate(string expression);
    }
}
