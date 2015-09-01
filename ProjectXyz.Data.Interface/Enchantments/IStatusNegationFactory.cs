using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IStatusNegationFactory
    {
        #region Method
        IStatusNegation Create(
            Guid id,
            Guid statId,
            Guid enchantmentStatusId);
        #endregion
    }
}
