using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Stats.Default.Calculations;
using ProjectXyz.Plugins.Features.Stats.Calculations;
using ProjectXyz.Plugins.Features.Stats.Default.Calculations;

using IStringExpressionEvaluator = ProjectXyz.Api.Framework.Math.IStringExpressionEvaluator;

namespace ProjectXyz.Plugins.Features.Stats.Default.Autofac
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
              .RegisterType<StatDefinitionToTermMappingRepositoryFacade>()
              .AsImplementedInterfaces()
              .SingleInstance();
            builder
                .Register(c => StatDefinitionToTermMappingRepository.None)
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatDefinitionToTermMappingRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => StatDefinitionToCalculationMappingRepository.None)
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}