using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Specialized
{
    public sealed class NotifyCollectionChangedEventArgs
    {
        #region Constructors
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
            : this(action)
        {
            NewItems = newItems;
            OldItems = oldItems;
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
            : this(action)
        {
            NewItems = action == NotifyCollectionChangedAction.Add ? changedItems : null;
            OldItems = action == NotifyCollectionChangedAction.Remove ? changedItems : null;
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object item)
            : this(action)
        {
            NewItems = action == NotifyCollectionChangedAction.Add ? new [] { item } : null;
            OldItems = action == NotifyCollectionChangedAction.Remove ? new[] { item } : null;
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
        {
            Action = action;
        }
        #endregion

        #region Properties
        public NotifyCollectionChangedAction Action { get; private set; }

        public IList NewItems { get; private set; }

        public IList OldItems { get; private set; }
        #endregion
    }
}
