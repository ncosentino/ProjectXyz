using System;
using System.Linq;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Core.Items;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public class ItemSaver : IItemSaver
    {
        #region Fields
        private readonly IEnchantmentSaver _enchantmentSaver;
        #endregion

        #region Constructors
        private ItemSaver(IEnchantmentSaver enchantmentSaver)
        {
            Contract.Requires<ArgumentNullException>(enchantmentSaver != null);
            _enchantmentSaver = enchantmentSaver;
        }
        #endregion

        #region Methods
        public static IItemSaver Create(IEnchantmentSaver enchantmentSaver)
        {
            Contract.Requires<ArgumentNullException>(enchantmentSaver != null);
            Contract.Ensures(Contract.Result<IItemSaver>() != null);

            return new ItemSaver(enchantmentSaver);
        }

        public IItemStore Save(IItem source)
        {
            var destination = ItemStore.Create(
                source.Id,
                source.Name, 
                source.InventoryGraphicResource, 
                source.ItemType, 
                source.MaterialTypeId,
                source.SocketTypeId,
                source.EquippableSlots);

            destination.Stats.Add(source.Stats);

            destination.Enchantments.Add(source.Enchantments.Select(x => _enchantmentSaver.Save(x)));

            destination.SocketedItems.Add(source.SocketedItems.Select(Save));

            destination.Requirements.Class = source.Requirements.Class;
            destination.Requirements.Level = source.Requirements.Level;
            destination.Requirements.Race = source.Requirements.Race;

            return destination;
        }
        #endregion
    }
}
