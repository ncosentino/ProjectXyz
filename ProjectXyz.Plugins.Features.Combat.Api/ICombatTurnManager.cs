using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public interface ICombatTurnManager : IReadOnlyCombatTurnManager
    {        
        void ProgressTurn(
            IFilterContext filterContext,
            int turns);
        
        void Reset();
    }
}
