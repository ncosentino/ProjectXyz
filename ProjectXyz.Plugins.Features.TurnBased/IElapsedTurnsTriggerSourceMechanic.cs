using System.Threading.Tasks;

using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IElapsedTurnsTriggerSourceMechanic : ITriggerSourceMechanic
    {
        Task UpdateAsync(ITurnInfo turnInfo);
    }
}