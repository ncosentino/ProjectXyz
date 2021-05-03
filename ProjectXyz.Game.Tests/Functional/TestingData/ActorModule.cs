using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class ActorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .RegisterType<AdditionalActorBehaviorProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class ActorIdentifiers : IActorIdentifiers
    {
        public IIdentifier FilterContextActorStatsIdentifier { get; } = new StringIdentifier("actor-stats");

        public IIdentifier ActorTypeIdentifier { get; } = new StringIdentifier("actor");

        public IIdentifier AnimationStandBack { get; } = new StringIdentifier("animation-stand-back");

        public IIdentifier AnimationStandForward { get; } = new StringIdentifier("animation-stand-forward");

        public IIdentifier AnimationStandLeft { get; } = new StringIdentifier("animation-stand-left");

        public IIdentifier AnimationStandRight { get; } = new StringIdentifier("animation-stand-right");

        public IIdentifier AnimationWalkBack { get; } = new StringIdentifier("animation-walk-back");

        public IIdentifier AnimationWalkForward { get; } = new StringIdentifier("animation-walk-forward");

        public IIdentifier AnimationWalkLeft { get; } = new StringIdentifier("animation-walk-left");

        public IIdentifier AnimationWalkRight { get; } = new StringIdentifier("animation-walk-right");

        public IIdentifier AnimationDeath { get; } = new StringIdentifier("animation-death");

        public IIdentifier AnimationCastBack { get; } = new StringIdentifier("animation-cast-back");

        public IIdentifier AnimationCastForward { get; } = new StringIdentifier("animation-cast-forward");

        public IIdentifier AnimationCastLeft { get; } = new StringIdentifier("animation-cast-left");

        public IIdentifier AnimationCastRight { get; } = new StringIdentifier("animation-cast-right");
    }

    public sealed class AdditionalActorBehaviorProvider : IDiscoverableActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            var inventory = new ItemContainerBehavior(new StringIdentifier("Inventory"));
            var hasItemContainers = new HasItemContainersBehavior();
            hasItemContainers.AddItemContainer(inventory);
            yield return hasItemContainers;

            var canEquip = new CanEquipBehavior(new[]
            {
                new StringIdentifier("head"),
                new StringIdentifier("body"),
                new StringIdentifier("left hand"),
                new StringIdentifier("right hand"),
                new StringIdentifier("amulet"),
                new StringIdentifier("ring1"),
                new StringIdentifier("ring2"),
                new StringIdentifier("shoulders"),
                new StringIdentifier("hands"),
                new StringIdentifier("waist"),
                new StringIdentifier("feet"),
                new StringIdentifier("legs"),
                new StringIdentifier("back"),
            });
            yield return canEquip;
        }
    }
}
