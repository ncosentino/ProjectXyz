
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

        public IIdentifier AnimationStandBack { get; } = new StringIdentifier("animation-stand-back");

        public IIdentifier AnimationStandForward { get; } = new StringIdentifier("animation-stand-forward");

        public IIdentifier AnimationStandLeft { get; } = new StringIdentifier("animation-stand-left");

        public IIdentifier AnimationStandRight { get; } = new StringIdentifier("animation-stand-right");

        public IIdentifier AnimationWalkBack { get; } = new StringIdentifier("animation-walk-back");

        public IIdentifier AnimationWalkForward { get; } = new StringIdentifier("animation-walk-forward");

        public IIdentifier AnimationWalkLeft { get; } = new StringIdentifier("animation-walk-left");

        public IIdentifier AnimationWalkRight { get; } = new StringIdentifier("animation-walk-right");

        public IIdentifier AnimationDeath { get; } = new StringIdentifier("animation-death");

        public IIdentifier AnimationDirectionBack => new StringIdentifier("back");

        public IIdentifier AnimationDirectionForward => new StringIdentifier("forward");

        public IIdentifier AnimationDirectionLeft => new StringIdentifier("left");

        public IIdentifier AnimationDirectionRight => new StringIdentifier("right");

        public IIdentifier AnimationDirectionPlaceholder => new StringIdentifier("$direction$");
    }
}
