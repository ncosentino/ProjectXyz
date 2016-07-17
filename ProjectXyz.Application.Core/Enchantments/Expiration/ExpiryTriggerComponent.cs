using ProjectXyz.Application.Interface.Enchantments.Expiration;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Application.Core.Enchantments.Expiration
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