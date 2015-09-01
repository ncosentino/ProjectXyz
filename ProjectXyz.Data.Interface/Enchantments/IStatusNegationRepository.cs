using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IStatusNegationRepository
    {
        #region Method
        IEnumerable<IStatusNegation> GetAll();

        IStatusNegation GetForId(Guid id);

        IStatusNegation GetForStatDefinitionId(Guid statDefinitionId);

        IStatusNegation GetForEnchantmentStatusId(Guid enchantmentStatusId);

        IStatusNegation Add(
            Guid id,
            Guid statDefinitionId,
            Guid enchantmentStatusId);

        void RemoveById(Guid id);
        #endregion
    }
}
