using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentDefinitionWeatherGrouping : IEnchantmentDefinitionWeatherGrouping
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _enchantmentDefinitionId;
        private readonly Guid _weatherTypeGroupingId;
        #endregion

        #region Constructors
        private EnchantmentDefinitionWeatherGrouping(
            Guid id,
            Guid enchantmentDefinitionId,
            Guid weatherTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(weatherTypeGroupingId != Guid.Empty);

            _id = id;
            _enchantmentDefinitionId = enchantmentDefinitionId;
            _weatherTypeGroupingId = weatherTypeGroupingId;
        }
        #endregion

        #region Properties
        public Guid Id 
        {
            get { return _id; }
        }

        public Guid EnchantmentDefinitionId
        {
            get { return _enchantmentDefinitionId; }
        }

        public Guid WeatherTypeGroupingId
        {
            get { return _weatherTypeGroupingId; }
        }
        #endregion

        #region Methods
        public static IEnchantmentDefinitionWeatherGrouping Create(
            Guid id,
            Guid enchantmentDefinitionId,
            Guid weatherTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(weatherTypeGroupingId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentDefinitionWeatherGrouping>() != null);

            return new EnchantmentDefinitionWeatherGrouping(
                id, 
                enchantmentDefinitionId, 
                weatherTypeGroupingId);
        }
        #endregion
    }
}
