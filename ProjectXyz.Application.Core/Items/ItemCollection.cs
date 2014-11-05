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

        public void Add(IItem item)
        {
            _items.Add(item);
        }

        public void AddRange(IEnumerable<IItem> items)
        {
            _items.AddRange(items);
        }

        public void Remove(IItem item)
        {
            _items.Remove(item);
        }

        public void RemoveRange(IEnumerable<IItem> items)
        {
            foreach (var item in items)
            {
                Remove(item);
            }
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
