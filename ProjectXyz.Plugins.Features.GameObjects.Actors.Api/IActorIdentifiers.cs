using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IActorIdentifiers
    {
        IIdentifier FilterContextActorStatsIdentifier { get; }

        IIdentifier ActorTypeIdentifier { get; }

        IIdentifier AnimationStandBack { get; }

        IIdentifier AnimationStandForward { get; }

        IIdentifier AnimationStandLeft { get; }

        IIdentifier AnimationStandRight { get; }

        IIdentifier AnimationWalkBack { get; }

        IIdentifier AnimationWalkForward { get; }

        IIdentifier AnimationWalkLeft { get; }

        IIdentifier AnimationWalkRight { get; }
    }
}