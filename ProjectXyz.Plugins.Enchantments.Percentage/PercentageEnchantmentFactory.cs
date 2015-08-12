using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public sealed class PercentageEnchantmentFactory : IPercentageEnchantmentFactory
    {
        #region Constructors
        private PercentageEnchantmentFactory()
        {
        }
        #endregion

        #region Methods
        public static IPercentageEnchantmentFactory Create()
        {
            var factory = new PercentageEnchantmentFactory();
            return factory;
        }

        public IPercentageEnchantment Create(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            TimeSpan remainingDuration,
            Guid statId,
            double value)
        {
            var enchantment = PercentageEnchantment.Create(
                id,
                statusTypeId,
                triggerId,
                remainingDuration,
                statId,
                value);
            return enchantment;
        }
        #endregion
    }
}
