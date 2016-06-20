using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegation
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
