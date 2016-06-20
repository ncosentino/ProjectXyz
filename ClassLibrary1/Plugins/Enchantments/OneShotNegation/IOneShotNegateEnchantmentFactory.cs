using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Plugins.Enchantments.OneShotNegation
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
