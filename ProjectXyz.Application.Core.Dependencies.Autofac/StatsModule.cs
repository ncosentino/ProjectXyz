using System.Collections.Generic;
using System.Linq;
using Autofac;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Application.Stats.Core.Calculations;
using ProjectXyz.Application.Stats.Interface.Calculations;

namespace ProjectXyz.Application.Core.Dependencies.Autofac
{
    public sealed class StatsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new StringExpressionEvaluatorWrapper(
                    c.Resolve<Framework.Interface.Math.IStringExpressionEvaluator>(),
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
                .RegisterType<MutableStatsProvider>()
                .AsImplementedInterfaces();
            ////.SingleInstance(); // *NOT* a single instance
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