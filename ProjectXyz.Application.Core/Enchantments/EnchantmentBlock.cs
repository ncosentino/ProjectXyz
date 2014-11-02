using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.ExtensionMethods;

namespace ProjectXyz.Application.Core.Enchantments
{
    public class EnchantmentBlock : MutableEnchantmentCollection, IEnchantmentBlock
    {
        #region Constructors
        protected EnchantmentBlock()
            : this(new IEnchantment[0])
        {
        }

        protected EnchantmentBlock(IEnumerable<IEnchantment> enchantments)
            : base(enchantments)
        {
        }
        #endregion

        #region Methods
        new public static IEnchantmentBlock Create()
        {
            return new EnchantmentBlock();
        }

        new public static IEnchantmentBlock Create(IEnumerable<IEnchantment> enchantments)
        {
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
        #endregion
    }
}
