using System.Threading.Tasks;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class ElapsedActionsTriggerMechanicSystem : IDiscoverableSystem
    {
        private readonly IElapsedActionsTriggerSourceMechanic _elapsedActionsTriggerSourceMechanic;

        public ElapsedActionsTriggerMechanicSystem(IElapsedActionsTriggerSourceMechanic elapsedActionsTriggerSourceMechanic)
        {
            _elapsedActionsTriggerSourceMechanic = elapsedActionsTriggerSourceMechanic;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<IActionInfo>>()
                .Value;
            await _elapsedActionsTriggerSourceMechanic
                .UpdateAsync(turnInfo)
                .ConfigureAwait(false);
        }
    }
}
