using System.Collections.Generic;
using System.Linq;
using Autofac;
using ProjectXyz.Api.Stats;
using ProjectXyz.Application.Stats.Core.Calculations;
using ProjectXyz.Application.Stats.Interface.Calculations;
using ProjectXyz.Framework.Interface;
using IExpressionStatDefinitionDependencyFinder =
    ProjectXyz.Application.Stats.Core.Calculations.IExpressionStatDefinitionDependencyFinder;

namespace ProjectXyz.Application.Core.Dependencies.Autofac
{
    public sealed class StatsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new StringExpressionEvaluatorWrapper(
                    c.Resolve<ProjectXyz.Framework.Interface.Math.IStringExpressionEvaluator>(),
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
                        .Resolve<IStatDefinitionToTermMappingRepository>()
                        .GetStatDefinitionIdToTermMappings()
                        .ToDictionary(x => x.StateDefinitionId, x => x.Term);
                    var statDefinitionIdToCalculationMapping = new Dictionary<IIdentifier, string>();
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
        }
    }
}