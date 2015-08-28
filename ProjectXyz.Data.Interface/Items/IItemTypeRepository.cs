using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemTypeRepository
    {
        #region Methods
        void Add(IItemType itemStore);

        void RemoveById(Guid id);

        IItemType GetById(Guid id);

        IEnumerable<IItemType> GetAll();
        #endregion
    }
}
