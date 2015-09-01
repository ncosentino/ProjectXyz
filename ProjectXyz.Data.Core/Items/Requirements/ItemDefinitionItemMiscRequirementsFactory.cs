using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Core.Items.Requirements
{
    public sealed class ItemDefinitionItemMiscRequirementsFactory : IItemDefinitionItemMiscRequirementsFactory
    {
        #region Constructors
        private ItemDefinitionItemMiscRequirementsFactory()
        {
        }
        #endregion
        
        #region Methods
        public static IItemDefinitionItemMiscRequirementsFactory Create()
        {
            var factory = new ItemDefinitionItemMiscRequirementsFactory();
            return factory;
        }

        public IItemDefinitionItemMiscRequirements Create(
            Guid id,
            Guid itemDefinitionId,
            Guid itemMiscRequirementsId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(itemMiscRequirementsId != Guid.Empty);

            Contract.Ensures(Contract.Result<IItemDefinitionItemMiscRequirements>() != null);

            var itemDefinitionItemMiscRequirements = ItemDefinitionItemMiscRequirements.Create(
                id,
                itemDefinitionId,
                itemMiscRequirementsId);
            return itemDefinitionItemMiscRequirements;
        }
        #endregion
    }
}