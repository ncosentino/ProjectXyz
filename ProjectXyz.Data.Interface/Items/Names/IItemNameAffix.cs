using System;

namespace ProjectXyz.Data.Interface.Items.Names
{
    public interface IItemNameAffix
    {
        #region Properties
        Guid Id { get; }

        bool IsPrefix { get; }

        Guid ItemTypeGroupingId { get; }

        Guid MagicTypeGroupingId { get; }

        Guid NameStringResourceId { get; }
        #endregion
    }
}
