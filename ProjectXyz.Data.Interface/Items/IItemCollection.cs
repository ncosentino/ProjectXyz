using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemCollection : IEnumerable<IItem>
    {
        #region Properties
        int Count { get; }
        #endregion
    }
}
