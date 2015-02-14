using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Core.Enchantments;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemStore : IItemStore
    {
        #region Fields
        private readonly IMutableStatCollection _stats;
        private readonly IMutableEnchantmentCollection _enchantments;
        private readonly IRequirements _requirements;
        private readonly IMutableItemStoreCollection _socketedItems;
        private readonly List<string> _equippableSlots;
        
        private Guid _id;
        #endregion

        #region Constructors
        private ItemStore(Guid id, string name, string inventoryGraphicResource, string itemType, Guid materialTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name.Trim().Length > 0);
            Contract.Requires<ArgumentNullException>(inventoryGraphicResource != null);
            Contract.Requires<ArgumentException>(inventoryGraphicResource.Trim().Length > 0);
            Contract.Requires<ArgumentNullException>(itemType != null);
            Contract.Requires<ArgumentException>(itemType.Trim().Length > 0);
            Contract.Requires<ArgumentException>(materialTypeId != Guid.Empty);

            _stats = StatCollection.Create();
            _enchantments = EnchantmentCollection.Create();
            _requirements = Items.Requirements.Create();
            _socketedItems = ItemStoreCollection.Create();
            _equippableSlots = new List<string>();

            _id = id;
            Name = name;
            InventoryGraphicResource = inventoryGraphicResource;
            MaterialTypeId = materialTypeId;
            ItemType = itemType;
        }
        #endregion

        #region Properties
        public string Name { get; private set; }

        public string InventoryGraphicResource { get; private set; }

        public Guid MagicTypeId { get; private set; }

        public string ItemType { get; private set; }

        public Guid MaterialTypeId { get; private set; }

        public IMutableStatCollection Stats
        {
            get { return _stats; }
        }

        public IMutableEnchantmentCollection Enchantments
        {
            get { return _enchantments; }
        }

        public IRequirements Requirements
        {
            get { return _requirements; }
        }

        public IMutableItemStoreCollection SocketedItems
        {
            get { return _socketedItems; }
        }

        public IList<string> EquippableSlots
        {
            get { return _equippableSlots; }
        }

        public Guid Id
        {
            get { return _id; }
        }
        #endregion

        #region Methods
        public static IItemStore Create(Guid id, string name, string inventoryGraphicResource, string itemType, Guid materialTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name.Trim().Length > 0);
            Contract.Requires<ArgumentNullException>(inventoryGraphicResource != null);
            Contract.Requires<ArgumentException>(inventoryGraphicResource.Trim().Length > 0);
            Contract.Requires<ArgumentNullException>(itemType != null);
            Contract.Requires<ArgumentException>(itemType.Trim().Length > 0);
            Contract.Requires<ArgumentException>(materialTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemStore>() != null);
            return new ItemStore(id, name, inventoryGraphicResource, itemType, materialTypeId);
        }
        #endregion
    }
}
