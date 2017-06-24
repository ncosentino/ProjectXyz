
using System.Collections.Generic;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Api.Stats.Calculations.Plugins
{
    public interface IStatCalculationPlugin : IPlugin
    {
        IReadOnlyCollection<IStatExpressionInterceptor> StatExpressionInterceptors { get; }
    }
}
