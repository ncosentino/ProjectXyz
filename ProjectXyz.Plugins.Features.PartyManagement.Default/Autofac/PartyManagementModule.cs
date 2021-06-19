using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.PartyManagement.Default.Autofac
{
    public sealed class PartyManagementModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<RosterManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<RosterBehaviorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}