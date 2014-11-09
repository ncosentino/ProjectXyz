using System;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Application.Core.Items
{
    public class ItemSaver : IItemSaver
    {
        #region Constructors
        private ItemSaver()
        {
        }
        #endregion

        #region Methods
        public static IItemSaver Create()
        {
            Contract.Ensures(Contract.Result<IItemSaver>() != null);
            return new ItemSaver();
        }

        public ProjectXyz.Data.Interface.Items.IItem Save(IItem source)
        {
            var destination = ProjectXyz.Data.Core.Items.Item.Create();

            destination.Id = source.Id;
            destination.Name = source.ItemType;
            destination.MagicType = source.MagicType;
            ////destination.MaterialType = source.Material.MaterialType;
            destination.Name = source.Name;

            foreach (var slot in source.EquippableSlots)
            {
                destination.EquippableSlots.Add(slot);
            }

            foreach (var stat in source.Stats)
            {
                destination.Stats.Add(stat);
            }

            return destination;
        }
        #endregion
    }
}
