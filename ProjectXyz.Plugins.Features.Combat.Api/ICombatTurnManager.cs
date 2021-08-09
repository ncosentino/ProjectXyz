using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;
using System.Threading.Tasks;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public interface ICombatTurnManager : IObservableCombatTurnManager
    {        
        Task ProgressTurnAsync(
            IFilterContext filterContext,
            int turns);

        Task StartCombatAsync(IFilterContext filterContext);

        Task EndCombatAsync(
            IEnumerable<IGameObject> winningTeam,
            IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> losingTeams);
    }
}
