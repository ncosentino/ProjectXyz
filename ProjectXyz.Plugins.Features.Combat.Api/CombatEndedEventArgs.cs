using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public sealed class CombatEndedEventArgs : EventArgs
    {

        public CombatEndedEventArgs(
            IEnumerable<IGameObject> winningTeam,
            IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> losingTeams)
        {
            WinningTeam = winningTeam.ToArray();
            LosingTeams = losingTeams;
        }

        public IReadOnlyCollection<IGameObject> WinningTeam { get; }

        public IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> LosingTeams { get; }
    }
}
