using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors.Serialization.Newtonsoft;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Autofac
{
    public sealed class CommonBehaviorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<HasStatsBehaviorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasEnchantmentsBehaviorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasEchantmentsGameObjectsForBehavior>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemContainerGameObjectsForBehavior>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EquipmentGameObjectsForBehavior>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<HasMutableStatsBehaviorSerializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasEnchantmentsBehaviorSerializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CanEquipBehaviorSerializer>()
                .AsImplementedInterfaces()
                .SingleInstance();            
        }
    }
}
