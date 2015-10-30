using System;
using System.Collections.Generic;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketPatternDefinitionEnchantmentRepository
    {
        #region Methods
        ISocketPatternEnchantment Add(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid enchantmentDefinitionId);

        void RemoveById(Guid id);

        ISocketPatternEnchantment GetById(Guid id);

        IEnumerable<ISocketPatternEnchantment> GetAll();

        IEnumerable<ISocketPatternEnchantment> GetBySocketPatternId(Guid socketPatternId);
        #endregion
    }
}