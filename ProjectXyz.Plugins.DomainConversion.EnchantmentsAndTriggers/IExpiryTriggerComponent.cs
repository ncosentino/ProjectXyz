using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers
{
    public interface IExpiryTriggerComponent : IExpiryComponent
    {
        IComponent TriggerComponent { get; }
    }
}