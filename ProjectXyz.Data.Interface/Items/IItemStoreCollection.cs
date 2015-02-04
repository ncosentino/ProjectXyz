using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemStoreCollection : IEnumerable<IItemStore>
    {
        #region Properties
        int Count { get; }
        #endregion
    }
}
