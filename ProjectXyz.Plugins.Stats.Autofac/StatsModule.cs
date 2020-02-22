using System.Collections.Generic;
using System.Linq;
using Autofac;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Stats.Calculations;
using IStringExpressionEvaluator = ProjectXyz.Api.Framework.Math.IStringExpressionEvaluator;

namespace ProjectXyz.Plugins.Stats.Autofac
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c => new StringExpressionEvaluatorWrapper(
                    c.Resolve<IStringExpressionEvaluator>(),
                    true))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatCalculationExpressionNodeFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ExpressionStatDefinitionDependencyFinder>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatDefinitionToTermConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatDefinitionToCalculationConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatCalculationNodeCreator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatCalculator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MutableStatsProviderFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => StatDefinitionToTermMappingRepository.None)
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => StatDefinitionToCalculationMappingRepository.None)
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}