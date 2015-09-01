using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentTriggerFactory : IEnchantmentTriggerFactory
    {
        #region Constructors
        private EnchantmentTriggerFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentTriggerFactory Create()
        {
            var factory = new EnchantmentTriggerFactory();
            return factory;
        }

        public IEnchantmentTrigger Create(
            Guid id,
            Guid nameStringResourceId)
        {
            var enchantmentTrigger = EnchantmentTrigger.Create(
                id,
                nameStringResourceId);
            return enchantmentTrigger;
        }
        #endregion
    }
}
