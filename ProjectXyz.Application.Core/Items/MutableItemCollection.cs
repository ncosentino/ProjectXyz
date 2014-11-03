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
    public class MutableItemCollection : ItemCollection, IMutableItemCollection
    {
        #region Constructors
        protected MutableItemCollection()
            : this(new IItem[0])
        {
        }

        protected MutableItemCollection(IEnumerable<IItem> items)
            : base(items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
        }
        #endregion

        #region Methods
        public static IMutableItemCollection Create()
        {
            return new MutableItemCollection();
        }

        public static IMutableItemCollection Create(IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            return new MutableItemCollection(items);
        }

        public void Add(IItem item)
        {
            base.AddItem(item);
        }

        public void AddRange(IEnumerable<IItem> items)
        {
            base.AddItemsRange(items);
        }

        public void Remove(IItem item)
        {
            base.RemoveItem(item);
        }

        public void RemoveRange(IEnumerable<IItem> items)
        {
            base.RemoveItemsRange(items);
        }

        public void Clear()
        {
            base.ClearItems();
        }
        #endregion
    }
}
