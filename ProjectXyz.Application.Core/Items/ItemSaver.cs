using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemSaver : IItemSaver
    {
        #region Fields
        private readonly IEnchantmentSaver _enchantmentSaver;
        private readonly IItemEnchantmentRepository _itemEnchantmentRepository;
        private readonly IItemStoreRepository _itemStoreRepository;
        private readonly IItemStoreItemMiscRequirementsRepository _itemStoreItemMiscRequirementsRepository;
        private readonly IItemMiscRequirementsRepository _itemMiscRequirementsRepository;
        private readonly IItemStatRequirementsRepository _itemStatRequirementsRepository;
        private readonly ISocketedItemRepository _socketedItemRepository;
        private readonly IItemStatRepository _itemStatRepository;
        private readonly IStatRepository _statRepository;
        private readonly IItemNamePartRepository _itemNamePartRepository;
        #endregion

        #region Constructors
        private ItemSaver(
            IEnchantmentSaver enchantmentSaver,
            IItemEnchantmentRepository itemEnchantmentRepository,
            IItemStoreRepository itemStoreRepository,
            IItemStoreItemMiscRequirementsRepository itemStoreItemMiscRequirementsRepository,
            IItemMiscRequirementsRepository itemMiscRequirementsRepository,
            IItemStatRequirementsRepository itemStatRequirementsRepository,
            ISocketedItemRepository socketedItemRepository,
            IItemStatRepository itemStatRepository,
            IStatRepository statRepository,
            IItemNamePartRepository itemNamePartRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentSaver != null);
            Contract.Requires<ArgumentNullException>(itemEnchantmentRepository != null);
            Contract.Requires<ArgumentNullException>(itemStoreRepository != null);
            Contract.Requires<ArgumentNullException>(itemStoreItemMiscRequirementsRepository != null);
            Contract.Requires<ArgumentNullException>(itemMiscRequirementsRepository != null);
            Contract.Requires<ArgumentNullException>(itemStatRequirementsRepository != null);
            Contract.Requires<ArgumentNullException>(socketedItemRepository != null);
            Contract.Requires<ArgumentNullException>(itemStatRepository != null);
            Contract.Requires<ArgumentNullException>(statRepository != null);
            Contract.Requires<ArgumentNullException>(itemNamePartRepository != null);

            _enchantmentSaver = enchantmentSaver;
            _itemEnchantmentRepository = itemEnchantmentRepository;
            _itemStoreRepository = itemStoreRepository;
            _itemStoreItemMiscRequirementsRepository = itemStoreItemMiscRequirementsRepository;
            _itemMiscRequirementsRepository = itemMiscRequirementsRepository;
            _itemStatRequirementsRepository = itemStatRequirementsRepository;
            _socketedItemRepository = socketedItemRepository;
            _itemStatRepository = itemStatRepository;
            _statRepository = statRepository;
            _itemNamePartRepository = itemNamePartRepository;
        }
        #endregion

        #region Methods
        public static IItemSaver Create(
            IEnchantmentSaver enchantmentSaver,
            IItemEnchantmentRepository itemEnchantmentRepository,
            IItemStoreRepository itemStoreRepository,
            IItemStoreItemMiscRequirementsRepository itemStoreItemMiscRequirementsRepository,
            IItemMiscRequirementsRepository itemMiscRequirementsRepository,
            IItemStatRequirementsRepository itemStatRequirementsRepository,
            ISocketedItemRepository socketedItemRepository,
            IItemStatRepository itemStatRepository,
            IStatRepository statRepository,
            IItemNamePartRepository itemNamePartRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentSaver != null);
            Contract.Requires<ArgumentNullException>(itemEnchantmentRepository != null);
            Contract.Requires<ArgumentNullException>(itemStoreRepository != null);
            Contract.Requires<ArgumentNullException>(itemStoreItemMiscRequirementsRepository != null);
            Contract.Requires<ArgumentNullException>(itemMiscRequirementsRepository != null);
            Contract.Requires<ArgumentNullException>(itemStatRequirementsRepository != null);
            Contract.Requires<ArgumentNullException>(socketedItemRepository != null);
            Contract.Requires<ArgumentNullException>(itemStatRepository != null);
            Contract.Requires<ArgumentNullException>(statRepository != null);
            Contract.Requires<ArgumentNullException>(itemNamePartRepository != null);
            Contract.Ensures(Contract.Result<IItemSaver>() != null);

            var itemSaver = new ItemSaver(
                enchantmentSaver,
                itemEnchantmentRepository,
                itemStoreRepository,
                itemStoreItemMiscRequirementsRepository,
                itemMiscRequirementsRepository,
                itemStatRequirementsRepository,
                socketedItemRepository,
                itemStatRepository,
                statRepository,
                itemNamePartRepository);
            return itemSaver;
        }

        public IItemStore Save(IItem source)
        {
            // FIXME: this name deleting/saving seems a bit shitty...
            var itemNamePartId = source.ItemNameParts.First().PartId;
            _itemNamePartRepository.RemoveByPartId(itemNamePartId);
            
            foreach (var itemNamePart in source.ItemNameParts)
            {
                _itemNamePartRepository.Add(
                    itemNamePart.Id,
                    itemNamePart.PartId,
                    itemNamePart.NameStringResourceId,
                    itemNamePart.Order);
            }
            
            var itemStore = _itemStoreRepository.Add(
                source.Id,
                source.ItemDefinitionId,
                itemNamePartId,
                source.InventoryGraphicResourceId,
                source.MagicTypeId,
                source.ItemTypeId,
                source.MaterialTypeId,
                source.SocketTypeId);

            foreach (var stat in source.Stats)
            {
                var statId = stat.Id;
                
                _statRepository.Add(
                    statId,
                    stat.StatDefinitionId,
                    stat.Value);
                _itemStatRepository.Add(
                    Guid.NewGuid(),
                    source.Id,
                    statId);
            }

            foreach (var enchantment in source.Enchantments)
            {
                _enchantmentSaver.Save(enchantment);
                _itemEnchantmentRepository.Add(
                    Guid.NewGuid(),
                    source.Id,
                    enchantment.Id);
            }

            foreach (var socketedItem in source.SocketedItems)
            {
                var socketedItemStore = Save(socketedItem);
                _socketedItemRepository.Add(
                    Guid.NewGuid(),
                    source.Id,
                    socketedItemStore.Id);
            }

            var requirements = source.Requirements;
            foreach (var stat in requirements.Stats)
            {
                _statRepository.Add(
                    stat.Id,
                    stat.StatDefinitionId,
                    stat.Value);
                _itemStatRequirementsRepository.Add(
                    Guid.NewGuid(),
                    source.Id,
                    stat.Id);
            }

            var itemMiscRequirements = _itemMiscRequirementsRepository.Add(
                Guid.NewGuid(),
                requirements.RaceDefinitionId,
                requirements.ClassDefinitionId);

            _itemStoreItemMiscRequirementsRepository.Add(
                Guid.NewGuid(),
                source.Id,
                itemMiscRequirements.Id);

            return itemStore;
        }
        #endregion
    }
}
