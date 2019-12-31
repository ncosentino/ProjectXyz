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
                .Register(c =>
                {
                    var statDefinitionIdToTermMapping = c
                        .Resolve<IEnumerable<IStatDefinitionToTermMappingRepository>>()
                        .SelectMany(x => x.GetStatDefinitionIdToTermMappings())
                        .ToDictionary(x => x.StateDefinitionId, x => x.Term);
                    var statDefinitionIdToCalculationMapping = c
                        .Resolve<IEnumerable<IStatDefinitionToCalculationMappingRepository>>()
                        .SelectMany(x => x.GetStatDefinitionIdToCalculationMappings())
                        .ToDictionary(x => x.StateDefinitionId, x => x.Calculation);
                    var statCalculationNodeCreator = new StatCalculationNodeCreator(
                        c.Resolve<IStatCalculationNodeFactory>(),
                        c.Resolve<IExpressionStatDefinitionDependencyFinder>(),
                        statDefinitionIdToTermMapping,
                        statDefinitionIdToCalculationMapping);
                    return statCalculationNodeCreator;
                })
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