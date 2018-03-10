using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Plugins.Triggers.Enchantments.Expiration
{
    public sealed class ExpiryTriggerComponent : IExpiryTriggerComponent
    {
        public ExpiryTriggerComponent(IComponent triggerComponent)
        {
            TriggerComponent = triggerComponent;
        }

        public IComponent TriggerComponent { get; }
    }
}