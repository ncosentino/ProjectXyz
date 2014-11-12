using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IMutableItemCollection : IItemCollection
    {
        #region Methods
        void Add(IEnumerable<IItem> items);
        
        bool Remove(IEnumerable<IItem> item);
        
        void Clear();
        #endregion
    }
}
