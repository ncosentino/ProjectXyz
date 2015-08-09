using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemContext : IItemContext
    {
        #region Fields
        private readonly IStatSocketTypeRepository _statSocketTypeRepository;
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly IEnchantmentFactory _enchantmentFactory;
        #endregion

        #region Constructors
        private ItemContext(
            IEnchantmentCalculator enchantmentCalculator,
            IEnchantmentFactory enchantmentFactory,
            IStatSocketTypeRepository statSocketTypeRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(enchantmentFactory != null);
            Contract.Requires<ArgumentNullException>(statSocketTypeRepository != null);

            _enchantmentCalculator = enchantmentCalculator;
            _enchantmentFactory = enchantmentFactory;
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
        
        public IEnchantmentFactory EnchantmentFactory
        {
            get { return _enchantmentFactory; }
        }
        #endregion

        #region Methods
        public static IItemContext Create(
            IEnchantmentCalculator enchantmentCalculator,
            IEnchantmentFactory enchantmentFactory,
            IStatSocketTypeRepository statSocketTypeRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(enchantmentFactory != null);
            Contract.Requires<ArgumentNullException>(statSocketTypeRepository != null);
            Contract.Ensures(Contract.Result<IItemContext>() != null);
            return new ItemContext(enchantmentCalculator, enchantmentFactory, statSocketTypeRepository);
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_enchantmentCalculator != null);
            Contract.Invariant(_enchantmentFactory != null);
        }
        #endregion
    }
}
