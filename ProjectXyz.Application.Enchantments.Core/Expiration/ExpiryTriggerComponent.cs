using ProjectXyz.Application.Enchantments.Interface.Expiration;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Application.Enchantments.Core.Expiration
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