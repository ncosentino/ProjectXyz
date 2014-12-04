using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentFactory : IEnchantmentFactory
    {
        #region Fields
        private readonly IEnchantmentContext _enchantmentContext;
        #endregion

        #region Constructors
        private EnchantmentFactory(IEnchantmentContext enchantmentContext)
        {
            Contract.Requires<ArgumentNullException>(enchantmentContext != null);

            _enchantmentContext = enchantmentContext;
        }
        #endregion

        #region Methods
        public static IEnchantmentFactory Create(IEnchantmentContext enchantmentContext)
        {
            Contract.Requires<ArgumentNullException>(enchantmentContext != null);
            Contract.Ensures(Contract.Result<IEnchantmentFactory>() != null);

            return new EnchantmentFactory(enchantmentContext);
        }

        public IEnchantment Create(IEnchantmentStore enchantmentStore)
        {
            return Enchantment.Create(_enchantmentContext, enchantmentStore);
        }

        public IEnchantment Create(
            Guid id,
            Guid statId, 
            Guid statusTypeId, 
            Guid triggerId, 
            Guid calculationId, 
            double value, 
            TimeSpan duration)
        {
            return Enchantment.Create(
                _enchantmentContext,
                id,
                statId,
                statusTypeId,
                triggerId,
                calculationId,
                value,
                duration);
        }
        #endregion
    }
}
