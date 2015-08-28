using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Core.Items.Requirements
{
    public sealed class ItemStatRequirementsFactory : IItemStatRequirementsFactory
    {
        #region Constructors
        private ItemStatRequirementsFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemStatRequirementsFactory Create()
        {
            var factory = new ItemStatRequirementsFactory();
            return factory;
        }

        /// <inheritdoc />
        public IItemStatRequirements Create(
            Guid id,
            Guid itemId,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemStatRequirements>() != null);

            var itemStatRequirements = ItemStatRequirements.Create(
                id,
                itemId,
                statId);
            return itemStatRequirements;
        }
        #endregion
    }
}
