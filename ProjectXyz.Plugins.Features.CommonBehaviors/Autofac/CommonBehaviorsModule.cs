using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Autofac
{
    public sealed class CommonBehaviorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<HasMutableStatsBehaviorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
