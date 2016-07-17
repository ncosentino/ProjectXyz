using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Application.Interface.Enchantments.Expiration
{
    public interface IExpiryTriggerComponent : IExpiryComponent
    {
        IComponent TriggerComponent { get; }
    }
}