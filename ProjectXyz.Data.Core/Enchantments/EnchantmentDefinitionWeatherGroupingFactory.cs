using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentDefinitionWeatherGroupingFactory : IEnchantmentDefinitionWeatherGroupingFactory
    {
        #region Constructors
        private EnchantmentDefinitionWeatherGroupingFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentDefinitionWeatherGroupingFactory Create()
        {
            var factory = new EnchantmentDefinitionWeatherGroupingFactory();
            return factory;
        }

        public IEnchantmentDefinitionWeatherGrouping Create(
            Guid id,
            Guid enchantmentDefinitionId,
            Guid weatherTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(weatherTypeGroupingId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentDefinitionWeatherGrouping>() != null);

            return EnchantmentDefinitionWeatherGrouping.Create(
                id, 
                enchantmentDefinitionId, 
                weatherTypeGroupingId);
        }
        #endregion
    }
}
