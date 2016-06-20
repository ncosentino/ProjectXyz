using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Interface.Enchantments
{
    public interface IEnchantment
    {
        IIdentifier StatusTypeId { get; }

        IIdentifier TriggerId { get; }
    }
}