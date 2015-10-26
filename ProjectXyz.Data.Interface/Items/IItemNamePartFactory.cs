using System;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemNamePartFactory
    {
        #region Methods
        IItemNamePart Create(
            Guid id,
            Guid partId,
            Guid nameStringResourceId,
            int order);
        #endregion
    }
}
