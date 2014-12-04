using System;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentSaver : IEnchantmentSaver
    {
        #region Fields
        private readonly IEnchantmentStoreFactory _enchantmentStoreFactory;
        #endregion

        #region Constructors
        private EnchantmentSaver(IEnchantmentStoreFactory enchantmentStoreFactory)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStoreFactory != null);

            _enchantmentStoreFactory = enchantmentStoreFactory;
        }
        #endregion

        #region Methods
        public static IEnchantmentSaver Create(IEnchantmentStoreFactory enchantmentStoreFactory)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStoreFactory != null);
            Contract.Ensures(Contract.Result<IEnchantmentSaver>() != null);
            
            return new EnchantmentSaver(enchantmentStoreFactory);
        }

        public IEnchantmentStore Save(IEnchantment source)
        {
            return _enchantmentStoreFactory.CreateEnchantmentStore(
                source.Id,
                source.StatId,
                source.CalculationId,
                source.TriggerId,
                source.StatusTypeId,
                source.RemainingDuration,
                source.Value);
        }
        #endregion
    }
}
