using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemTypeRepository
    {
        #region Methods
        IItemType Add(
            Guid id,
            Guid nameStringResourceId);

        void RemoveById(Guid id);

        IItemType GetById(Guid id);

        IEnumerable<IItemType> GetAll();
        #endregion
    }
}
