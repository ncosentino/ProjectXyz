using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemTypeGeneratorPlugin
    {
        #region Properties
        Guid Id { get; }

        Guid MagicTypeId { get; }

        string ItemGeneratorClassName { get; }
        #endregion
    }
}
