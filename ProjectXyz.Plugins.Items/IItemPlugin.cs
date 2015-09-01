using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Plugins.Items
{
    public interface IItemPlugin
    {
        #region Properties
        Guid MagicTypeId { get; }

        GenerateItemDelegate GenerateItemCallback { get; }
        #endregion
    }
}
