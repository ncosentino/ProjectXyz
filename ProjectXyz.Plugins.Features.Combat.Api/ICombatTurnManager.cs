using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public interface ICombatTurnManager : IObservableCombatTurnManager
    {        
        void ProgressTurn(
            IFilterContext filterContext,
            int turns);
        
        void StartCombat(IFilterContext filterContext);

        void EndCombat(
            IEnumerable<IGameObject> winningTeam,
            IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> losingTeams);
    }
}
