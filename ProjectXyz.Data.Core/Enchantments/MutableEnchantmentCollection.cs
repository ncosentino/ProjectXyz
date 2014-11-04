using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
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
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }
        #endregion

        #region Methods
        public static IMutableEnchantmentCollection Create()
        {
            Contract.Ensures(Contract.Result<IMutableEnchantmentCollection>() != null);
            return new MutableEnchantmentCollection();
        }

        public static IMutableEnchantmentCollection Create(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IMutableEnchantmentCollection>() != null);
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
