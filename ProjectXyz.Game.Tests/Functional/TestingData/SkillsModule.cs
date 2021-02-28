using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class SkillsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .RegisterType<SkillIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class SkillIdentifiers : ISkillIdentifiers
    {
        public IIdentifier SkillDefinitionIdentifier { get; } = new StringIdentifier("id");

        public IIdentifier SkillTypeIdentifier { get; } = new StringIdentifier("skill");

        public IIdentifier SkillSynergyIdentifier { get; } = new StringIdentifier("id");
    }
}
