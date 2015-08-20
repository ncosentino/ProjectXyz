using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;

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

                EventHandler<EventArgs> expiredHandler = (s, e) =>
                {
                    i--;
                    this.Remove((IEnchantment)s);
                };

                enchantment.Expired += expiredHandler;
                try
                {
                    enchantment.UpdateElapsedTime(elapsedTime);
                }
                finally
                {
                    enchantment.Expired -= expiredHandler;
                }
            }
        }
        #endregion
    }
}
