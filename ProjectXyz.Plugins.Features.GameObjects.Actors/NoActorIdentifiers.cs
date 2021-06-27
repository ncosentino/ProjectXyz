using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class NoActorIdentifiers : IActorIdentifiers
    {
        public IIdentifier ActorDefinitionIdentifier { get; } = new StringIdentifier("actor-id");

        public IIdentifier FilterContextActorStatsIdentifier { get; } = new StringIdentifier("actor-stats");

        public IIdentifier ActorTypeIdentifier { get; } = new StringIdentifier("actor");

        public IIdentifier AnimationStand { get; } = new StringIdentifier("animation-stand");

        public IIdentifier AnimationWalk { get; } = new StringIdentifier("animation-walk");

        public IIdentifier AnimationDeath { get; } = new StringIdentifier("animation-death");

        public IIdentifier AnimationDirectionBack => new StringIdentifier("back");

        public IIdentifier AnimationDirectionForward => new StringIdentifier("forward");

        public IIdentifier AnimationDirectionLeft => new StringIdentifier("left");

        public IIdentifier AnimationDirectionRight => new StringIdentifier("right");

        public IIdentifier AnimationDirectionPlaceholder => new StringIdentifier("$direction$");
    }
}
