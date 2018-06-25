using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments
{
    public interface IExpiryTriggerBehavior : IExpiryBehavior
    {
        IBehavior TriggerBehavior { get; }
    }
}