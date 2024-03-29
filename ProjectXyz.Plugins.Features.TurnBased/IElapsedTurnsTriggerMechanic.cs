﻿using System.Threading.Tasks;

using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IElapsedTurnsTriggerMechanic : ITriggerMechanic
    {
        Task<bool> UpdateAsync(ITurnInfo turnInfo);
    }
}