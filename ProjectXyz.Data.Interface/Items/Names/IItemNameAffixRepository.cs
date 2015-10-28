
using System;
using System.Collections.Generic;

namespace ProjectXyz.Data.Interface.Items.Names
{
    public interface IItemNameAffixRepository
    {
        #region Methods
        IItemNameAffix Add(
            Guid id,
            bool isPrefix,
            Guid itemTypeGroupingId,
            Guid magicTypeGroupingId,
            Guid nameStringResourceId);

        void RemoveById(Guid id);

        IItemNameAffix GetById(Guid id);
        
        IEnumerable<IItemNameAffix> GetAll();
        #endregion
    }
}
