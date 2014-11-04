using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.ExtensionMethods;

namespace ProjectXyz.Application.Core.Enchantments
{
    public class EnchantmentBlock : MutableEnchantmentCollection, IEnchantmentBlock
    {
        #region Fields
        private bool _deferCollectionModifiedEvent;
        #endregion

        #region Constructors
        protected EnchantmentBlock()
            : this(new IEnchantment[0])
        {
        }

        protected EnchantmentBlock(IEnumerable<IEnchantment> enchantments)
            : base(enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }
        #endregion

        #region Events
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        #region Methods
        new public static IEnchantmentBlock Create()
        {
            return new EnchantmentBlock();
        }

        new public static IEnchantmentBlock Create(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            return new EnchantmentBlock(enchantments);
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            for (int i = 0; i < this.Count; i++)
            {
                var enchantment = this[i];

                enchantment.UpdateElapsedTime(elapsedTime);
                if (enchantment.IsExpired())
                {
                    Remove(enchantment);
                    i--;
                }
            }
        }

        protected override void AddEnchantment(IEnchantment enchantment)
        {
            base.AddEnchantment(enchantment);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, enchantment);
        }

        protected override void AddEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            DeferCollectionModifiedEvent(() => base.AddEnchantments(enchantments));
            OnCollectionReset();
        }

        protected override void RemoveEnchantment(IEnchantment enchantment)
        {
            base.RemoveEnchantment(enchantment);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, enchantment);
        }

        protected override void RemoveEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            DeferCollectionModifiedEvent(() => base.RemoveEnchantments(enchantments));
            OnCollectionReset();
        }

        protected override void ClearEnchantments()
        {
            DeferCollectionModifiedEvent(() => base.ClearEnchantments());
            OnCollectionReset();
        }

        private void OnCollectionReset()
        {
            if (_deferCollectionModifiedEvent)
            {
                return;
            }

            var handler = CollectionChanged;
            if (handler != null)
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                handler.Invoke(this, args);
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, IEnchantment enchantment)
        {
            if (_deferCollectionModifiedEvent)
            {
                return;
            }

            var handler = CollectionChanged;
            if (handler != null)
            {
                var args = new NotifyCollectionChangedEventArgs(
                    action,
                    enchantment);
                handler.Invoke(this, args);
            }
        }

        private void DeferCollectionModifiedEvent(Action action)
        {
            Contract.Requires<ArgumentNullException>(action != null);

            bool saveDefer = _deferCollectionModifiedEvent;
            _deferCollectionModifiedEvent = true;
            try
            {
                action();
            }
            finally
            {
                _deferCollectionModifiedEvent = saveDefer;
            }
        }
        #endregion
    }
}
