using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Core.Items.Requirements
{
    public sealed class ItemDefinitionStatRequirementsFactory : IItemDefinitionStatRequirementsFactory
    {
        #region Constructors
        private ItemDefinitionStatRequirementsFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemDefinitionStatRequirementsFactory Create()
        {
            var factory = new ItemDefinitionStatRequirementsFactory();
            return factory;
        }

        /// <inheritdoc />
        public IItemDefinitionStatRequirements Create(
            Guid id,
            Guid itemDefinitionId,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemDefinitionStatRequirements>() != null);

            var itemDefinitionStatRequirements = ItemDefinitionStatRequirements.Create(
                id,
                itemDefinitionId,
                statId);
            return itemDefinitionStatRequirements;
        }
        #endregion
    }
}
