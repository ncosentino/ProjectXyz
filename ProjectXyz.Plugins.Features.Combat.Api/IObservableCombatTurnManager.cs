using System;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public interface IObservableCombatTurnManager : IReadOnlyCombatTurnManager
    {
        event EventHandler<TurnProgressedEventArgs> TurnProgressed;

        event EventHandler<CombatStartedEventArgs> CombatStarted;
    }
}
