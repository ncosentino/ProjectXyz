using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemContext : IItemContext
    {
        #region Fields
        private readonly IStatSocketTypeRepository _statSocketTypeRepository;
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly IEnchantmentContext _enchantmentContext;
        #endregion

        #region Constructors
        private ItemContext(
            IEnchantmentCalculator enchantmentCalculator, 
            IEnchantmentContext enchantmentContext,
            IStatSocketTypeRepository statSocketTypeRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(enchantmentContext != null);
            Contract.Requires<ArgumentNullException>(statSocketTypeRepository != null);

            _enchantmentCalculator = enchantmentCalculator;
            _enchantmentContext = enchantmentContext;
            _statSocketTypeRepository = statSocketTypeRepository;
        }
        #endregion

        #region Properties
        public IStatSocketTypeRepository StatSocketTypeRepository
        {
            get { return _statSocketTypeRepository; }
        }

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
        public static IItemContext Create(
            IEnchantmentCalculator enchantmentCalculator, 
            IEnchantmentContext enchantmentContext,
            IStatSocketTypeRepository statSocketTypeRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(enchantmentContext != null);
            Contract.Requires<ArgumentNullException>(statSocketTypeRepository != null);
            Contract.Ensures(Contract.Result<IItemContext>() != null);
            return new ItemContext(enchantmentCalculator, enchantmentContext, statSocketTypeRepository);
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
