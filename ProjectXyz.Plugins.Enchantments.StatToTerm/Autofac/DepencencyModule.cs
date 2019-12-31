using System.Collections.Generic;
using System.Linq;
using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm.Autofac
{
    public sealed class DepencencyModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c =>
                {
                    var statDefinitionIdToTermMapping = c
                        .Resolve<IEnumerable<IStatDefinitionToTermMappingRepository>>()
                        .SelectMany(x => x.GetStatDefinitionIdToTermMappings())
                        .ToDictionary(x => x.StateDefinitionId, x => x.Term);
                    var statToTermExpressionInterceptorFactory = new StatToTermExpressionInterceptorFactory(
                        statDefinitionIdToTermMapping,
                        0);
                    return statToTermExpressionInterceptorFactory;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
