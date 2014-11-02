using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Interface.Enchantments;

namespace ProjectXyz.Core.Enchantments
{
    public class MutableEnchantmentCollection : 
        EnchantmentCollection, 
        IMutableEnchantmentCollection
    {
        #region Constructors
        protected MutableEnchantmentCollection()
            : this(new IEnchantment[0])
        {
        }

        protected MutableEnchantmentCollection(IEnumerable<IEnchantment> enchantments)
            : base(enchantments)
        {
        }
        #endregion

        #region Methods
        public static IMutableEnchantmentCollection Create()
        {
            return new MutableEnchantmentCollection();
        }

        public static IMutableEnchantmentCollection Create(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            return new MutableEnchantmentCollection(enchantments);
        }

        public void Add(IEnchantment enchantment)
        {
            AddEnchantment(enchantment);
        }

        public void AddRange(IEnumerable<IEnchantment> enchantments)
        {
            AddEnchantments(enchantments);
        }

        public void Remove(IEnchantment enchantment)
        {
            RemoveEnchantment(enchantment);
        }

        public void RemoveRange(IEnumerable<IEnchantment> enchantments)
        {
            RemoveEnchantments(enchantments);
        }

        public void Clear()
        {
            ClearEnchantments();
        }

        public new System.Collections.IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
        #endregion
    }
}
