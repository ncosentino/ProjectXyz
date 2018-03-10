using System.Linq;
using Autofac;
using ProjectXyz.Plugins.Api.Stats.Bounded;

namespace ProjectXyz.Plugins.Stats.Calculations.Bounded.Autofac
{
    public sealed class SharedComponentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c =>
                {
                    var statDefinitionIdToBoundsMapping = c.Resolve<IStatDefinitionIdToBoundsMappingRepository>()
                        .GetStatDefinitionIdToBoundsMappings()
                        .ToDictionary(x => x.StatDefinitiondId, x => x.StatBounds);
                    var statBoundsExpressionInterceptor = new StatBoundsExpressionInterceptor(
                        statDefinitionIdToBoundsMapping,
                        int.MaxValue);
                    return statBoundsExpressionInterceptor;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
