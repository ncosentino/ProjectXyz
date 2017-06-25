using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers
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