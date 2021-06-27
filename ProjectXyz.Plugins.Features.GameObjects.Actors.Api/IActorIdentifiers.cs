using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IActorIdentifiers
    {
        IIdentifier FilterContextActorStatsIdentifier { get; }

        IIdentifier ActorTypeIdentifier { get; }

        IIdentifier ActorDefinitionIdentifier { get; }

        // FIXME: are animations game specific?
        IIdentifier AnimationStand { get; }

        IIdentifier AnimationWalk { get; }

        IIdentifier AnimationDeath { get; }

        IIdentifier AnimationDirectionBack { get; }

        IIdentifier AnimationDirectionForward { get; }

        IIdentifier AnimationDirectionLeft { get; }

        IIdentifier AnimationDirectionRight { get; }

        IIdentifier AnimationDirectionPlaceholder { get; }
    }
}