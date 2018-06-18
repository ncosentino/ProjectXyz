using System;

namespace ProjectXyz.Api.Framework.Math
{
    public interface IStringExpressionEvaluator : IDisposable
    {
        double Evaluate(string expression);
    }
}
