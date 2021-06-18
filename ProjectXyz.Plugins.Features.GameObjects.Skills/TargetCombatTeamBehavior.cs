using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class TargetCombatTeamBehavior : BaseBehavior, ITargetCombatTeamBehavior
    {
        public TargetCombatTeamBehavior(
            IEnumerable<IIdentifier> affectedTeamIds)
        {
            AffectedTeamIds = affectedTeamIds.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> AffectedTeamIds { get; }
    }
}
