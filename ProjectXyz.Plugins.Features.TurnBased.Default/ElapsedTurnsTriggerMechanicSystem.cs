﻿using System.Threading.Tasks;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class ElapsedTurnsTriggerMechanicSystem : IDiscoverableSystem
    {
        private readonly IElapsedTurnsTriggerSourceMechanic _elapsedTurnsTriggerSourceMechanic;

        public ElapsedTurnsTriggerMechanicSystem(IElapsedTurnsTriggerSourceMechanic elapsedTurnsTriggerSourceMechanic)
        {
            _elapsedTurnsTriggerSourceMechanic = elapsedTurnsTriggerSourceMechanic;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;
            await _elapsedTurnsTriggerSourceMechanic
                .UpdateAsync(turnInfo)
                .ConfigureAwait(false);
        }
    }
}
