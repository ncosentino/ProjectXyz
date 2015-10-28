using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Contracts;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Names;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IItemFactoryContract))]
    public interface IItemFactory
    {
        #region Methods
        IItem Create(IItemContext context,
            Guid id,
            Guid itemDefinitionId,
            IItemMetaData itemMetaData,
            IEnumerable<IItemNamePart> itemNameParts,
            IItemRequirements itemRequirements,
            IEnumerable<IStat> stats,
            IEnumerable<IEnchantment> enchantments,
            IEnumerable<IItemAffix> affixes,
            IEnumerable<IItem> socketedItems,
            IEnumerable<Guid> equippableSlots);
        #endregion
    }
}
