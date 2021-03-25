using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.Combat.Default.Autofac
{
    public sealed class CombatModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<CombatTurnManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
