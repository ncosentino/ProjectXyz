using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Autofac
{
    public sealed class SkillModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<HasSkillsGameObjectsForBehavior>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillBehaviorsInterceptorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillBehaviorsProviderFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillSynergyRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillDefinitionRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HasPassiveSkillsGetEnchantmentsHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
