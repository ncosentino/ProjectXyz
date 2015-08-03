using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.ExtensionMethods;

namespace ProjectXyz.Application.Core.Enchantments
{
    public class EnchantmentBlock : EnchantmentCollection, IEnchantmentBlock
    {
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
        
        #region Methods
        new public static IEnchantmentBlock Create()
        {
            Contract.Ensures(Contract.Result<IEnchantmentBlock>() != null);
            return new EnchantmentBlock();
        }

        new public static IEnchantmentBlock Create(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IEnchantmentBlock>() != null);
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
                    this.Remove(enchantment);
                    i--;
                }
            }
        }
        #endregion
    }
}
