using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Autofac
{
    public sealed class EffectModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            RegisterExecutors(builder);

            RegisterEffectComponents(builder);
        }

        private void RegisterExecutors(ContainerBuilder builder)
        {
            builder
                .RegisterType<ParallelEffectExecutorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SequentialEffectExecutorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private void RegisterEffectComponents(ContainerBuilder builder)
        {
            builder
                .RegisterType<EnchantTargetsConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InflictDamageConverter>()
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
                .RegisterType<PassiveEnchantmentConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PassiveSkillEffectConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();            
        }
    }
}
