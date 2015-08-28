using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemTypeFactory
    {
        #region Methods
        IItemType Create(
            Guid id,
            Guid nameStringResourceId);
        #endregion
    }
}
