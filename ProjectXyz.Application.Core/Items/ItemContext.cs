using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemContext : IItemContext
    {
        #region Fields
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly IEnchantmentContext _enchantmentContext;
        #endregion

        #region Constructors
        private ItemContext(IEnchantmentCalculator enchantmentCalculator, IEnchantmentContext enchantmentContext)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(enchantmentContext != null);

            _enchantmentCalculator = enchantmentCalculator;
            _enchantmentContext = enchantmentContext;
        }
        #endregion

        #region Properties
        public IEnchantmentCalculator EnchantmentCalculator
        {
            get { return _enchantmentCalculator; }
        }
        
        public IEnchantmentContext EnchantmentContext
        {
            get { return _enchantmentContext; }
        }
        #endregion

        #region Methods
        public static IItemContext Create(IEnchantmentCalculator enchantmentCalculator, IEnchantmentContext enchantmentContext)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(enchantmentContext != null);
            Contract.Ensures(Contract.Result<IItemContext>() != null);
            return new ItemContext(enchantmentCalculator, enchantmentContext);
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_enchantmentCalculator != null);
            Contract.Invariant(_enchantmentContext != null);
        }
        #endregion
    }
}
