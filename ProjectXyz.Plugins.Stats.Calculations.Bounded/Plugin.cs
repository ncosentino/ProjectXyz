using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Stats.Bounded;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Api.Stats.Calculations.Plugins;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.Stats.Calculations.Bounded
{
    public sealed class Plugin : IStatCalculationPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            var statDefinitionIdToBoundsMapping = pluginArgs
                .GetFirst<IComponent<IStatDefinitionIdToBoundsMappingRepository>>()
                .Value
                .GetStatDefinitionIdToBoundsMappings()
                .ToDictionary(x => x.StatDefinitiondId, x => x.StatBounds);

            SharedComponents = new ComponentCollection(new[]
            {
                new GenericComponent<IStatExpressionInterceptor>(new StatBoundsExpressionInterceptor(statDefinitionIdToBoundsMapping, int.MaxValue)),
            });
        }


        public IComponentCollection SharedComponents { get; }
    }
}
