using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public class EnchantmentCollection : IMutableEnchantmentCollection
    {
        #region Fields
        private readonly List<IEnchantmentStore> _enchantments;
        #endregion

        #region Constructors
        protected EnchantmentCollection()
            : this(new IEnchantmentStore[0])
        {
        }

        protected EnchantmentCollection(IEnumerable<IEnchantmentStore> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);

            _enchantments = new List<IEnchantmentStore>();
            _enchantments.AddRange(enchantments);
        }
        #endregion

        #region Properties
        public int Count
        {
            get { return _enchantments.Count; }
        }

        public IEnchantmentStore this[int index]
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

        public static IMutableEnchantmentCollection Create(IEnumerable<IEnchantmentStore> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IMutableEnchantmentCollection>() != null);
            return new EnchantmentCollection(enchantments);
        }

        public void Add(IEnumerable<IEnchantmentStore> enchantments)
        {
            _enchantments.AddRange(enchantments);
        }

        public virtual bool Remove(IEnumerable<IEnchantmentStore> enchantments)
        {
            bool removedAny = false;
            foreach (var enchantment in enchantments)
            {
                removedAny |= _enchantments.Remove(enchantment);
            }

            return removedAny;
        }

        public virtual void Clear()
        {
            _enchantments.Clear();
        }

        public bool Contains(IEnchantmentStore enchantment)
        {
            return _enchantments.Contains(enchantment);
        }

        public IEnumerator<IEnchantmentStore> GetEnumerator()
        {
            return _enchantments.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _enchantments.GetEnumerator();
        }
        #endregion
    }
}
