using System;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketPatternEnchantmentFactory
    {
        #region Methods
        ISocketPatternEnchantment Create(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid enchantmentDefinitionId);
        #endregion
    }
}
