using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentStatusFactory : IEnchantmentStatusFactory
    {
        #region Constructors
        private EnchantmentStatusFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentStatusFactory Create()
        {
            var factory = new EnchantmentStatusFactory();
            return factory;
        }

        public IEnchantmentStatus Create(
            Guid id,
            Guid nameStringResourceId)
        {
            var enchantmentStatus = EnchantmentStatus.Create(
                id,
                nameStringResourceId);
            return enchantmentStatus;
        }
        #endregion
    }
}
