using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default.Autofac
{
    public sealed class HandlersModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<HasEnchantmentsGetEnchantmentsHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasEquipmentGetEnchantmentsHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasItemContainersGetEnchantmentsHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasStatsStatCalculatorHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ComponentsHandlerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ComponentsForTargetComponentHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ComponentsForTargetComponentFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
