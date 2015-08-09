using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;

namespace ProjectXyz.Application.Core.Actors
{
    public sealed class ActorContext : IActorContext
    {
        #region Fields
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        #endregion

        #region Constructors
        private ActorContext(IEnchantmentCalculator enchantmentCalculator)
        {
            _enchantmentCalculator = enchantmentCalculator;
        }
        #endregion

        #region Properties
        public IEnchantmentCalculator EnchantmentCalculator
        {
            get { return _enchantmentCalculator; }
        }
        #endregion

        #region Methods
        public static IActorContext Create(IEnchantmentCalculator enchantmentCalculator)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Ensures(Contract.Result<IActorContext>() != null);
            return new ActorContext(enchantmentCalculator);
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_enchantmentCalculator != null);
        }
        #endregion
    }
}
