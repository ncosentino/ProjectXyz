using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public class EnchantmentFactory : IEnchantmentFactory
    {
        #region Constructors
        private EnchantmentFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentFactory Create()
        {
            Contract.Ensures(Contract.Result<IEnchantmentFactory>() != null);

            return new EnchantmentFactory();
        }

        public IEnchantment CreateEnchantment()
        {
            return Enchantment.Create();
        }
        #endregion
    }
}
