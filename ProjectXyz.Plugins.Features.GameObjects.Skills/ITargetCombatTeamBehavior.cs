using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ITargetCombatTeamBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> AffectedTeamIds { get; }
    }
}
