using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Application.Enchantments.Interface.Expiration
{
    public interface IExpiryTriggerComponent : IExpiryComponent
    {
        IComponent TriggerComponent { get; }
    }
}