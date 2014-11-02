using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public abstract class EnchantmentCollection : IEnchantmentCollection
    {
        #region Fields
        private readonly List<IEnchantment> _enchantments;
        #endregion

        #region Constructors
        protected EnchantmentCollection()
        {
            _enchantments = new List<IEnchantment>();
        }

        protected EnchantmentCollection(IEnumerable<IEnchantment> enchantments)
            : this()
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            _enchantments.AddRange(enchantments);
        }
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
        public bool Contains(IEnchantment enchantment)
        {
            return _enchantments.Contains(enchantment);
        }

        public IEnumerator<IEnchantment> GetEnumerator()
        {
            return _enchantments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _enchantments.GetEnumerator();
        }

        protected void AddEnchantment(IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
            _enchantments.Add(enchantment);
        }

        protected void AddEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);

            foreach (var enchantment in enchantments)
            {
                AddEnchantment(enchantment);
            }
        }

        protected void RemoveEnchantment(IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
            _enchantments.Remove(enchantment);
        }

        protected void RemoveEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }

        protected void ClearEnchantments()
        {
            _enchantments.Clear();
        }
        #endregion
    }
}
