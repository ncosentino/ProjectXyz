using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public interface ICombatTurnManager : IObservableCombatTurnManager
    {        
        void ProgressTurn(
            IFilterContext filterContext,
            int turns);
        
        void StartCombat(IFilterContext filterContext);
    }
}
