using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegation
{
    public interface IOneShotNegateEnchantmentFactory
    {
        #region Methods
        IOneShotNegateEnchantment Create(
            IIdentifier statusTypeId,
            IIdentifier triggerId,
            IIdentifier statDefinitionId);
        #endregion
    }
}
