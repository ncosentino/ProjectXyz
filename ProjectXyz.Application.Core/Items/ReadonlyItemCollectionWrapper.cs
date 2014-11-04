using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ReadonlyItemCollectionWrapper : IReadonlyItemCollection
    {
        #region Fields
        private readonly IItemCollection _items;
        #endregion

        #region Constructors
        private ReadonlyItemCollectionWrapper(IItemCollection items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            _items = items;
        }
        #endregion
        
        #region Properties
        public int Count
        {
            get { return _items.Count; }
        }
        #endregion

        #region Methods
        public static IReadonlyItemCollection Create(IItemCollection items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            Contract.Ensures(Contract.Result<IReadonlyItemCollection>() != null);
            return new ReadonlyItemCollectionWrapper(items);
        }

        public IEnumerator<IItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        #endregion
    }
}
