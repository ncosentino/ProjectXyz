using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentContext : IEnchantmentContext
    {
        #region Constructors
        private EnchantmentContext()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentContext Create()
        {
            Contract.Ensures(Contract.Result<IEnchantmentContext>() != null);
            return new EnchantmentContext();
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
        }
        #endregion
    }
}
