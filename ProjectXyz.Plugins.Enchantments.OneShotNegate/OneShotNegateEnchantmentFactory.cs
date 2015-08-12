using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantmentFactory : IOneShotNegateEnchantmentFactory
    {
        #region Constructors
        private OneShotNegateEnchantmentFactory()
        {
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentFactory Create()
        {
            var factory = new OneShotNegateEnchantmentFactory();
            return factory;
        }

        public IOneShotNegateEnchantment Create(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            TimeSpan remainingDuration,
            Guid statId)
        {
            var enchantment = OneShotNegateEnchantment.Create(
                id,
                statusTypeId,
                triggerId,
                remainingDuration,
                statId);
            return enchantment;
        }
        #endregion
    }
}
