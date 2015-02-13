using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IItemManager
    {
        #region Methods
        IItem GetItemById(Guid itemId, IItemContext itemContext);
        #endregion
    }
}
