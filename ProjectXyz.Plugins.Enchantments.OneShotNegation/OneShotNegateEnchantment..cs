using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegation
{
    public sealed class OneShotNegateEnchantment : IOneShotNegateEnchantment
    {
        #region Constructors
        public OneShotNegateEnchantment(
            IIdentifier statusTypeId,
            IIdentifier triggerId,
            IIdentifier statDefinitionId)
        {
            StatusTypeId = statusTypeId;
            TriggerId = triggerId;
            StatDefinitionId = statDefinitionId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IIdentifier StatusTypeId { get; }

        /// <inheritdoc />
        public IIdentifier TriggerId { get; }

        /// <inheritdoc />
        public IIdentifier StatDefinitionId { get; }
        #endregion
    }
}
