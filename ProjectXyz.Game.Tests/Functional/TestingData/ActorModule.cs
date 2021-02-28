using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Behaviors;
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
