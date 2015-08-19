using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public sealed class AdditiveEnchantmentFactory : IAdditiveEnchantmentFactory
    {
        #region Constructors
        private AdditiveEnchantmentFactory()
        {
        }
        #endregion

        #region Methods
        public static IAdditiveEnchantmentFactory Create()
        {
            var factory = new AdditiveEnchantmentFactory();
            return factory;
        }

        public IAdditiveEnchantment Create(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            IEnumerable<Guid> weatherIds,
            TimeSpan remainingDuration,
            Guid statId,
            double value)
        {
            var enchantment = AdditiveEnchantment.Create(
                id,
                statusTypeId,
                triggerId,
                weatherIds,
                remainingDuration,
                statId,
                value);
            return enchantment;
        }
        #endregion
    }
}
