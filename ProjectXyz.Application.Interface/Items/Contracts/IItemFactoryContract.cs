using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IItemFactory))]
    public sealed class IItemFactoryContract : IItemFactory
    {
        #region Methods
        public IItem Create(
            IItemContext context,
            Guid id,
            Guid itemDefinitionId,
            IItemMetaData itemMetaData,
            IItemRequirements itemRequirements,
            IEnumerable<IStat> stats,
            IEnumerable<IEnchantment> enchantments,
            IEnumerable<IItemAffix> affixes,
            IEnumerable<IItem> socketedItems,
            IEnumerable<Guid> equippableSlots)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(itemMetaData != null);
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(affixes != null);
            Contract.Requires<ArgumentNullException>(socketedItems != null);
            Contract.Requires<ArgumentNullException>(equippableSlots != null);
            Contract.Ensures(Contract.Result<IItem>() != null);

            return default(IItem);
        }
        #endregion
    }
}
