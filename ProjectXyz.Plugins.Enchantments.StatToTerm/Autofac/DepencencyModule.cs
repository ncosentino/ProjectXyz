using System.Collections.Generic;
using System.Linq;
using Autofac;
using ProjectXyz.Api.Stats;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm.Autofac
{
    public sealed class DepencencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c =>
                {
                    var statDefinitionIdToTermMapping = c.Resolve<IStatDefinitionToTermMappingRepository>()
                        .GetStatDefinitionIdToTermMappings()
                        .ToDictionary(
                            x => x.StateDefinitionId,
                            x => x.Term);
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
