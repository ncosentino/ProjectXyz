using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ITargetCombatTeamBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> AffectedTeams { get; }
    }

    public sealed class TargetCombatTeamBehavior : BaseBehavior, ITargetCombatTeamBehavior
    {
        public TargetCombatTeamBehavior(
            IEnumerable<IIdentifier> affectTeamIds)
        {
            AffectedTeams = affectTeamIds.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> AffectedTeams { get; }
    }
}
