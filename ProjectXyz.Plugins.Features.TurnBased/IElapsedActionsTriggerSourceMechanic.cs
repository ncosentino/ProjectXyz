using System.Threading.Tasks;

using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IElapsedActionsTriggerSourceMechanic : ITriggerSourceMechanic
    {
        Task UpdateAsync(IActionInfo actionInfo);
    }
}