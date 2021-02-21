using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
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
        }
    }
}
