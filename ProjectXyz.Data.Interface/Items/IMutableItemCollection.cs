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
        void Add(IItem item);
        
        void AddRange(IEnumerable<IItem> items);
        
        void Remove(IItem item);
        
        void RemoveRange(IEnumerable<IItem> item);
        
        void Clear();
        #endregion
    }
}
