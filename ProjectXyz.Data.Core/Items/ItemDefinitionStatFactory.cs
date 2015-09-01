using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemDefinitionStatFactory : IItemDefinitionStatFactory
    {
        #region Constructors
        private ItemDefinitionStatFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemDefinitionStatFactory Create()
        {
            var factory = new ItemDefinitionStatFactory();
            return factory;
        }

        /// <inheritdoc />
        public IItemDefinitionStat Create(
            Guid id,
            Guid itemDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue)
        {
            var itemDefinitionStat = ItemDefinitionStat.Create(
                id,
                itemDefinitionId,
                statDefinitionId,
                minimumValue,
                maximumValue);
            return itemDefinitionStat;
        }
        #endregion
    }
}
