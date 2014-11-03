using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public abstract class ItemCollection : IItemCollection
    {
        #region Fields
        private readonly List<IItem> _items;
        #endregion

        #region Constructors
        protected ItemCollection()
        {
            _items = new List<IItem>();
        }

        protected ItemCollection(IEnumerable<IItem> items)
            : this()
        {
            Contract.Requires<ArgumentNullException>(items != null);
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
        public IEnumerator<IItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        protected void AddItem(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            _items.Add(item);
        }

        protected void AddItemsRange(IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            _items.AddRange(items);
        }

        protected void RemoveItem(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            _items.Remove(item);
        }

        protected void RemoveItemsRange(IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            foreach (var item in items)
            {
                _items.Remove(item);
            }
        }

        protected void ClearItems()
        {
            _items.Clear();
        }
        #endregion
    }
}
