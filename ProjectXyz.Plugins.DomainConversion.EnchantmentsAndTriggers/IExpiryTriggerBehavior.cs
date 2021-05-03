using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments
{
    public interface IExpiryTriggerBehavior : IExpiryBehavior
    {
        IBehavior TriggerBehavior { get; }
    }
}