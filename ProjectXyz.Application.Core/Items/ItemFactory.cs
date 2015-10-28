using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Names;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemFactory : IItemFactory
    {
        #region Constructors
        private ItemFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemFactory Create()
        {
            var factory = new ItemFactory();
            return factory;
        }

        public IItem Create(
            IItemContext context,
            Guid id,
            Guid itemDefinitionId,
            IItemMetaData itemMetaData,
            IEnumerable<IItemNamePart> itemNameParts,
            IItemRequirements itemRequirements,
            IEnumerable<IStat> stats,
            IEnumerable<IEnchantment> enchantments,
            IEnumerable<IItemAffix> affixes,
            IEnumerable<IItem> socketedItems,
            IEnumerable<Guid> equippableSlots)
        {
            return Item.Create(
                context, 
                id,
                itemDefinitionId,
                itemMetaData,
                itemNameParts,
                itemRequirements,
                stats,
                enchantments,
                affixes,
                socketedItems,
                equippableSlots);
        }
        #endregion
    }
}
