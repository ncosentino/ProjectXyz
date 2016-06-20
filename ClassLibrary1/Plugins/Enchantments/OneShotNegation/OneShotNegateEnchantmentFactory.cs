using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Plugins.Enchantments.OneShotNegation
{
    public sealed class OneShotNegateEnchantmentFactory : IOneShotNegateEnchantmentFactory
    {
        #region Methods
        public IOneShotNegateEnchantment Create(
            IIdentifier statusTypeId,
            IIdentifier triggerId,
            IIdentifier statDefinitionId)
        {
            return new OneShotNegateEnchantment(
                statusTypeId,
                triggerId,
                statDefinitionId);
        }
        #endregion
    }
}
