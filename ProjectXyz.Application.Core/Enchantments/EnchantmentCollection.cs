using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using System.Collections.Specialized;
using System.Collections;

namespace ProjectXyz.Application.Core.Enchantments
{
    public class EnchantmentCollection : IMutableEnchantmentCollection
    {
        #region Fields
        private readonly List<IEnchantment> _enchantments;
        #endregion

        #region Constructors
        protected EnchantmentCollection()
            : this(new IEnchantment[0])
        {
        }

        protected EnchantmentCollection(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            
            _enchantments = new List<IEnchantment>();
            _enchantments.AddRange(enchantments);
        }
        #endregion

        #region Events
        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        #region Properties
        public int Count
        {
            get { return _enchantments.Count; }
        }

        public IEnchantment this[int index]
        {
            get { return _enchantments[index]; }
        }
        #endregion

        #region Methods
        public static IMutableEnchantmentCollection Create()
        {
            Contract.Ensures(Contract.Result<IMutableEnchantmentCollection>() != null);
            return new EnchantmentCollection();
        }

        public static IMutableEnchantmentCollection Create(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IMutableEnchantmentCollection>() != null);
            return new EnchantmentCollection(enchantments);
        }

        public virtual void Add(IEnumerable<IEnchantment> enchantments)
        {
            _enchantments.AddRange(enchantments);

            OnCollectionChanged(
                NotifyCollectionChangedAction.Add,
                enchantments.ToArray());
        }

        public virtual bool Remove(IEnumerable<IEnchantment> enchantments)
        {
            bool removedAny = false;
            foreach (var enchantment in enchantments)
            {
                removedAny |= _enchantments.Remove(enchantment);
            }

            if (removedAny)
            {
                OnCollectionChanged(
                    NotifyCollectionChangedAction.Remove,
                    enchantments.ToArray());
            }

            return removedAny;
        }

        public virtual void Clear()
        {
            _enchantments.Clear();
            OnCollectionReset();
        }

        public bool Contains(IEnchantment enchantment)
        {
            return _enchantments.Contains(enchantment);
        }

        public IEnumerator<IEnchantment> GetEnumerator()
        {
            return _enchantments.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _enchantments.GetEnumerator();
        }

        private void OnCollectionReset()
        {
            var handler = CollectionChanged;
            if (handler != null)
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                handler.Invoke(this, args);
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, IList enchantments)
        {
            var handler = CollectionChanged;
            if (handler != null)
            {
                var args = new NotifyCollectionChangedEventArgs(
                    action,
                    enchantments);
                handler.Invoke(this, args);
            }
        }
        #endregion       
    }
}
