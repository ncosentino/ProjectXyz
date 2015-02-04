using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public class ItemCollection : IMutableItemCollection
    {
        #region Fields
        private readonly List<IItem> _items;
        #endregion

        #region Constructors
        protected ItemCollection()
            : this(new IItem[0])
        {
        }

        protected ItemCollection(IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);

            _items = new List<IItem>();
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
        public static IMutableItemCollection Create()
        {
            Contract.Ensures(Contract.Result<IMutableItemCollection>() != null);
            return new ItemCollection();
        }

        public static IMutableItemCollection Create(IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            Contract.Ensures(Contract.Result<IMutableItemCollection>() != null);
            return new ItemCollection(items);
        }

        public void Add(IEnumerable<IItem> items)
        {
            _items.AddRange(items);
        }
        
        public bool Remove(IEnumerable<IItem> items)
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
