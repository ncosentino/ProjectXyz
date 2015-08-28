using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Core.Items.Affixes
{
    public sealed class ItemAffixDefinitionFactory : IItemAffixDefinitionFactory
    {
        #region Constructors
        private ItemAffixDefinitionFactory()
        {
        }
        #endregion
        
        #region Methods
        public static IItemAffixDefinitionFactory Create()
        {
            var factory = new ItemAffixDefinitionFactory();
            return factory;
        }

        /// <inheritdoc />
        public IItemAffixDefinition Create(
            Guid id,
            Guid nameStringResourceId,
            bool isPrefix,
            int minimumLevel,
            int maximumLevel)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumLevel >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(maximumLevel >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(maximumLevel >= minimumLevel);
            Contract.Ensures(Contract.Result<IItemAffixDefinition>() != null);

            var itemAffixDefinition = ItemAffixDefinition.Create(
                id,
                nameStringResourceId,
                isPrefix,
                minimumLevel,
                maximumLevel);
            return itemAffixDefinition;
        }
        #endregion
    }
}
