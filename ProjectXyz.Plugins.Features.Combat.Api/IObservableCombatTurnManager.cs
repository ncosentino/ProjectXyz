using System;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public interface IObservableCombatTurnManager : IReadOnlyCombatTurnManager
    {
        event EventHandler<TurnOrderEventArgs> TurnProgressed;
    }
}
