using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Items;
using ProjectXyz.Data.Core.Items.Affixes;
using ProjectXyz.Data.Core.Items.EquipSlots;
using ProjectXyz.Data.Core.Items.MagicTypes;
using ProjectXyz.Data.Core.Items.Materials;
using ProjectXyz.Data.Core.Items.Requirements;
using ProjectXyz.Data.Core.Items.Sockets;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Items.EquipSlots;
using ProjectXyz.Data.Interface.Items.MagicTypes;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Data.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Data.Sql.Items.Affixes;
using ProjectXyz.Data.Sql.Items.EquipSlots;
using ProjectXyz.Data.Sql.Items.MagicTypes;
using ProjectXyz.Data.Sql.Items.Materials;
using ProjectXyz.Data.Sql.Items.Requirements;
using ProjectXyz.Data.Sql.Items.Sockets;

namespace ProjectXyz.Data.Sql.Items
{
    public sealed class SqlItemDataManager : IItemDataManager
    {
        #region Fields
        private readonly IItemDefinitionRepository _itemDefinitionRepository;
        private readonly IItemDefinitionStatRepository _itemDefinitionStatRepository;
        private readonly IItemTypeEquipSlotTypeRepository _itemTypeEquipSlotTypeRepository;
        private readonly IItemDefinitionStatRequirementsRepository _itemDefinitionStatRequirementsRepository;
        private readonly IItemMiscRequirementsRepository _itemMiscRequirementsRepository;
        private readonly IItemDefinitionItemMiscRequirementsRepository _itemDefinitionItemMiscRequirementsRepository;
        private readonly IItemAffixDefinitionEnchantmentRepository _itemAffixDefinitionEnchantmentRepository;
        private readonly IItemAffixDefinitionRepository _itemAffixDefinitionRepository;
        private readonly IItemAffixDefinitionFilterRepository _itemAffixDefinitionFilterRepository;
        private readonly IMagicTypesRandomAffixesRepository _magicTypesRandomAffixesRepository;
        private readonly ISocketTypeRepository _socketTypeRepository;
        private readonly IMaterialTypeRepository _materialTypeRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IMagicTypeRepository _magicTypeRepository;
        private readonly IEquipSlotTypeRepository _equipSlotTypeRepository;
        #endregion

        #region Constructors
        private SqlItemDataManager(IDatabase database)
        {
            var itemTypeEquipSlotTypeFactory = ItemTypeEquipSlotTypeFactory.Create();
            _itemTypeEquipSlotTypeRepository = ItemTypeEquipSlotTypeRepository.Create(
                database,
                itemTypeEquipSlotTypeFactory);

            var itemDefinitionFactory = ItemDefinitionFactory.Create();
            _itemDefinitionRepository = ItemDefinitionRepository.Create(
                database,
                itemDefinitionFactory);

            var itemDefinitionStatFactory = ItemDefinitionStatFactory.Create();
            _itemDefinitionStatRepository = ItemDefinitionStatRepository.Create(
                database,
                itemDefinitionStatFactory);

            var itemMiscRequirementsFactory = ItemMiscRequirementsFactory.Create();
            _itemMiscRequirementsRepository = ItemMiscRequirementsRepository.Create(
                database,
                itemMiscRequirementsFactory);

            var itemDefinitionStatRequirementsFactory = ItemDefinitionStatRequirementsFactory.Create();
            _itemDefinitionStatRequirementsRepository = ItemDefinitionStatRequirementsRepository.Create(
                database,
                itemDefinitionStatRequirementsFactory);

            var itemDefinitionItemMiscRequirementsFactory = ItemDefinitionItemMiscRequirementsFactory.Create();
            _itemDefinitionItemMiscRequirementsRepository = ItemDefinitionItemMiscRequirementsRepository.Create(
                database,
                itemDefinitionItemMiscRequirementsFactory);

            var itemAffixDefinitionEnchantmentFactory = ItemAffixDefinitionEnchantmentFactory.Create();
            _itemAffixDefinitionEnchantmentRepository = ItemAffixDefinitionEnchantmentRepository.Create(
                database,
                itemAffixDefinitionEnchantmentFactory);

            var itemAffixDefinitionFactory = ItemAffixDefinitionFactory.Create();
            _itemAffixDefinitionRepository = ItemAffixDefinitionRepository.Create(
                database,
                itemAffixDefinitionFactory);

            _itemAffixDefinitionFilterRepository = ItemAffixDefinitionFilterRepository.Create(
                database,
                itemAffixDefinitionFactory);

            var magicTypesRandomAffixesFactory = MagicTypesRandomAffixesFactory.Create();
            _magicTypesRandomAffixesRepository = MagicTypesRandomAffixesRepository.Create(
                database,
                magicTypesRandomAffixesFactory);

            var socketTypeFactory = SocketTypeFactory.Create();
            _socketTypeRepository = SocketTypeRepository.Create(
                database,
                socketTypeFactory);

            var materialTypeFactory = MaterialTypeFactory.Create();
            _materialTypeRepository = MaterialTypeRepository.Create(
                database,
                materialTypeFactory);

            var itemTypeFactory = ItemTypeFactory.Create();
            _itemTypeRepository = ItemTypeRepository.Create(
                database,
                itemTypeFactory);

            var magicTypeFactory = MagicTypeFactory.Create();
            _magicTypeRepository = MagicTypeRepository.Create(
                database,
                magicTypeFactory);

            var equipSlotTypeFactory = EquipSlotTypeFactory.Create();
            _equipSlotTypeRepository = EquipSlotTypeRepository.Create(
                database,
                equipSlotTypeFactory);
        }
        #endregion

        #region Properties
        public IItemDefinitionRepository ItemDefinitions { get { return _itemDefinitionRepository; } }

        public IItemDefinitionStatRepository ItemDefinitionStat { get { return _itemDefinitionStatRepository;} }

        public IItemTypeEquipSlotTypeRepository ItemTypeEquipSlotType { get { return _itemTypeEquipSlotTypeRepository; } }

        public IItemDefinitionStatRequirementsRepository ItemDefinitionStatRequirements { get { return _itemDefinitionStatRequirementsRepository; } }

        public IItemMiscRequirementsRepository ItemMiscRequirements { get { return _itemMiscRequirementsRepository; } }

        public IItemDefinitionItemMiscRequirementsRepository ItemDefinitionItemMiscRequirements { get { return _itemDefinitionItemMiscRequirementsRepository; } }
        
        public IItemAffixDefinitionRepository ItemAffixDefinitions { get { return _itemAffixDefinitionRepository; } }

        public IItemAffixDefinitionFilterRepository ItemAffixDefinitionFilter { get { return _itemAffixDefinitionFilterRepository; } }

        public IItemAffixDefinitionEnchantmentRepository ItemAffixDefinitionEnchantment { get { return _itemAffixDefinitionEnchantmentRepository; } }

        public IMagicTypesRandomAffixesRepository MagicTypesRandomAffixes { get { return _magicTypesRandomAffixesRepository; } }

        public ISocketTypeRepository SocketTypes { get { return _socketTypeRepository; } }

        public IMaterialTypeRepository MaterialTypes { get { return _materialTypeRepository; } }

        public IItemTypeRepository ItemTypes { get { return _itemTypeRepository; } }

        public IMagicTypeRepository MagicTypes { get { return _magicTypeRepository; } }

        public IEquipSlotTypeRepository EquipSlotTypes { get { return _equipSlotTypeRepository; } }
        #endregion

        #region Methods
        public static IItemDataManager Create(IDatabase database)
        {
            var dataContext = new SqlItemDataManager(database);
            return dataContext;
        }
        #endregion
    }
}
