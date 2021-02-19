using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items
{
    public sealed class Item : IGameObject
    {
        public Item(
            IBehaviorManager behaviorManager,
            IHasMutableStatsBehavior hasStatsBehavior,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            Behaviors = hasStatsBehavior
                .Yield()
                .Concat(additionalBehaviors)
                .ToArray();
            Contract.Requires(
                Behaviors.TakeTypes<IHasStatsBehavior>().Count() == 1,
                $"Expecting only a single behavior for '{this}' to be " +
                $"'{typeof(IHasStatsBehavior)}'.");
            behaviorManager.Register(this, Behaviors);
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}