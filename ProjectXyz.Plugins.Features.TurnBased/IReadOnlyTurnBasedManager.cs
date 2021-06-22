﻿using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IReadOnlyTurnBasedManager
    {
        bool SyncTurnsFromElapsedTime { get; }

        bool ClearApplicableOnUpdate { get; }

        bool GlobalSync { get; }

        IReadOnlyCollection<IGameObject> GetApplicableGameObjects();

        IReadOnlyCollection<IGameObject> GetAllGameObjects();
    }
}