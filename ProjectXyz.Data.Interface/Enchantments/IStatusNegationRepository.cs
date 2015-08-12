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

        IStatusNegation GetForStatId(Guid statId);

        IStatusNegation GetForEnchantmentStatusId(Guid enchantmentStatusId);

        void Add(IStatusNegation statusNegation);

        void RemoveById(Guid id);
        #endregion
    }
}
