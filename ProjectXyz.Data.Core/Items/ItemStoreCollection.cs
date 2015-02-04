using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public class ItemStoreCollection : IMutableItemStoreCollection
    {
        #region Fields
        private readonly List<IItemStore> _items;
        #endregion

        #region Constructors
        protected ItemStoreCollection()
            : this(new IItemStore[0])
        {
        }

        protected ItemStoreCollection(IEnumerable<IItemStore> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);

            _items = new List<IItemStore>();
            _items.AddRange(items);
        }
        #endregion

        #region Properties
        public int Count
        {
            get { return _items.Count; }
        }
        #endregion

        #region Methods
        public static IMutableItemStoreCollection Create()
        {
            Contract.Ensures(Contract.Result<IMutableItemStoreCollection>() != null);
            return new ItemStoreCollection();
        }

        public static IMutableItemStoreCollection Create(IEnumerable<IItemStore> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            Contract.Ensures(Contract.Result<IMutableItemStoreCollection>() != null);
            return new ItemStoreCollection(items);
        }

        public void Add(IEnumerable<IItemStore> items)
        {
            _items.AddRange(items);
        }
        
        public bool Remove(IEnumerable<IItemStore> items)
        {
            bool removedAny = false;
            foreach (var item in items)
            {
                removedAny |= _items.Remove(item);
            }

            return removedAny;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public IEnumerator<IItemStore> GetEnumerator()
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
