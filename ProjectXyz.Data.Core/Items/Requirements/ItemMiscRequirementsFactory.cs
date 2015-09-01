using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Core.Items.Requirements
{
    public sealed class ItemMiscRequirementsFactory : IItemMiscRequirementsFactory
    {
        #region Constructors
        private ItemMiscRequirementsFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemMiscRequirementsFactory Create()
        {
            var factory = new ItemMiscRequirementsFactory();
            return factory;
        }

        /// <inheritdoc />
        public IItemMiscRequirements Create(
            Guid id,
            Guid? raceDefinitionId,
            Guid? classDefinitionId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(raceDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(classDefinitionId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemMiscRequirements>() != null);

            var itemMiscRequirements = ItemMiscRequirements.Create(
                id,
                raceDefinitionId,
                classDefinitionId);
            return itemMiscRequirements;
        }
        #endregion
    }
}
