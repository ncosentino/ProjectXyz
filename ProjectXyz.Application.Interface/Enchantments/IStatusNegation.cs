using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IStatusNegation
    {
        #region Properties
        IIdentifier StatDefinitionId { get; }

        IIdentifier EnchantmentStatusId { get; }
        #endregion
    }
}
