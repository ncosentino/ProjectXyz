using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantment
    {
        IIdentifier StatusTypeId { get; }

        IIdentifier TriggerId { get; }
    }
}