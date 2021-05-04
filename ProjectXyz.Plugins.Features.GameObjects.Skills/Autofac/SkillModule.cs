using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components;
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
                .RegisterType<BehaviorConverterFacade>()
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

            // Component converters
            builder
                .RegisterType<DisplayIconConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DisplayNameConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantTargetsConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InflictDamageConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillIdentifierConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillMutableStatsConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillTargetCombatTeamConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillTargetOriginConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillTargetPatternConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StaticResourceRequirementsConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<UseInCombatConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<UseOutOfCombatConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SkillCombinationConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ParallelSkillExecutorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SequentialSkillExecutorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PassiveEnchantmentConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
