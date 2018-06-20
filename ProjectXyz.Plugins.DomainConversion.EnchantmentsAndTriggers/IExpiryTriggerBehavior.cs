using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Triggers.Enchantments.Expiration
{
    public interface IExpiryTriggerBehavior : IExpiryBehavior
    {
        IBehavior TriggerBehavior { get; }
    }
}