using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Autofac
{
    public sealed class StatCalculationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TargetNavigator>()
                .As<ITargetNavigator>()
                .SingleInstance();
            builder
                .RegisterType<StatCalculationService>()
                .As<IStatCalculationService>()
                .SingleInstance();
        }
    }
}
