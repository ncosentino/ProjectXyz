using System.Threading.Tasks;

using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IElapsedActionsTriggerMechanic : ITriggerMechanic
    {
        Task<bool> UpdateAsync(IActionInfo actionInfo);
    }
}