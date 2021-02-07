using System.Linq;
using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.BoundedStats.Api;

namespace ProjectXyz.Plugins.Features.BoundedStats.Autofac
{
    public sealed class SharedComponentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var statDefinitionIdToBoundsMapping = c.Resolve<IReadOnlyStatDefinitionIdToBoundsMappingRepositoryFacade>()
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
