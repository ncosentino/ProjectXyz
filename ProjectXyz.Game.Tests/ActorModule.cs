using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Game.Tests
{
    public sealed class ActorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // FIXME: this kind of thing makes it REALLY hard to know if we 
            // have the right configuration in this library vs consuming 
            // libraries (i.e. ProjectXyz -> Macerus -> Macerus Unity). things 
            // might work great here but they might not have something like 
            // this registered and they NEED it for things to work as expected
            builder
                .RegisterType<AdditionalActorBehaviorProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class AdditionalActorBehaviorProvider : IAdditionalActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IGameObject gameObject)
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
