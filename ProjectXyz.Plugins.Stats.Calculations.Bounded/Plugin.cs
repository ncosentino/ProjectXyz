using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Stats.Bounded;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Api.Stats.Calculations.Plugins;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.Stats.Calculations.Bounded
{
    public sealed class Plugin : IStatCalculationPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            var statDefinitionIdToBoundsMapping = pluginArgs
                .GetFirst<IStatDefinitionIdToBoundsMappingRepository>()
                .GetStatDefinitionIdToBoundsMappings()
                .ToDictionary(x => x.StatDefinitiondId, x => x.StatBounds);

            StatExpressionInterceptors = new IStatExpressionInterceptor[]
            {
                new StatBoundsExpressionInterceptor(statDefinitionIdToBoundsMapping, int.MaxValue),
            };
        }

        public IReadOnlyCollection<IStatExpressionInterceptor> StatExpressionInterceptors { get; }
    }
}
