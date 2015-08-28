using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemType
    {
        #region Properties
        Guid Id { get; }

        Guid NameStringResourceId { get; }
        #endregion
    }
}
