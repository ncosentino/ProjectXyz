using System;

namespace ProjectXyz.Data.Interface.Items.Names
{
    public interface IItemNameAffixFactory
    {
        #region Methods
        IItemNameAffix Create(
            Guid id,
            bool isPrefix,
            Guid itemTypeGroupingId,
            Guid magicTypeGroupingId,
            Guid nameStringResourceId);
        #endregion
    }
}
