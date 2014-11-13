using System;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public class EnchantmentSaver : IEnchantmentSaver
    {
        #region Constructors
        private EnchantmentSaver()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentSaver Create()
        {
            Contract.Ensures(Contract.Result<IEnchantmentSaver>() != null);
            return new EnchantmentSaver();
        }

        public ProjectXyz.Data.Interface.Enchantments.IEnchantment Save(IEnchantment source)
        {
            var destination = ProjectXyz.Data.Core.Enchantments.Enchantment.Create();

            destination.CalculationId = source.CalculationId;
            destination.RemainingDuration = source.RemainingDuration;
            destination.StatId = source.StatId;
            destination.Value = source.Value;
            destination.Trigger = source.Trigger;
            destination.StatusType = source.StatusType;

            return destination;
        }
        #endregion
    }
}
