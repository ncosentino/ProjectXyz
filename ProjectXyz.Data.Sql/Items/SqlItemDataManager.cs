using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Items;
using ProjectXyz.Data.Core.Items.Affixes;
using ProjectXyz.Data.Core.Items.EquipSlots;
using ProjectXyz.Data.Core.Items.MagicTypes;
using ProjectXyz.Data.Core.Items.Materials;
using ProjectXyz.Data.Core.Items.Names;
using ProjectXyz.Data.Core.Items.Requirements;
using ProjectXyz.Data.Core.Items.Sockets;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Items.EquipSlots;
using ProjectXyz.Data.Interface.Items.MagicTypes;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Data.Interface.Items.Names;
using ProjectXyz.Data.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Data.Sql.Items.Affixes;
using ProjectXyz.Data.Sql.Items.EquipSlots;
using ProjectXyz.Data.Sql.Items.MagicTypes;
using ProjectXyz.Data.Sql.Items.Materials;
using ProjectXyz.Data.Sql.Items.Names;
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
        private readonly IItemAffixDefinitionMagicTypeGroupingRepository _itemAffixDefinitionMagicTypeGroupingRepository;
        private readonly IMagicTypesRandomAffixesRepository _magicTypesRandomAffixesRepository;
        private readonly ISocketTypeRepository _socketTypeRepository;
        private readonly IMaterialTypeRepository _materialTypeRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IItemTypeGroupingRepository _itemTypeGroupingRepository;
        private readonly IItemNamePartRepository _itemNamePartRepository;
        private readonly IItemNamePartFactory _itemNamePartFactory;
        private readonly IItemNameAffixRepository _itemNameAffixRepository;
        private readonly IItemNameAffixFilter _itemNameAffixFilter;
        private readonly IItemTypeGeneratorPluginRepository _itemTypeGeneratorPluginRepository;
        private readonly IMagicTypeRepository _magicTypeRepository;
        private readonly IMagicTypeGroupingRepository _magicTypeGroupingRepository;
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

            var itemAffixDefinitionMagicTypeGroupingFactory = ItemAffixDefinitionMagicTypeGroupingFactory.Create();
            _itemAffixDefinitionMagicTypeGroupingRepository = ItemAffixDefinitionMagicTypeGroupingRepository.Create(
                database,
                itemAffixDefinitionMagicTypeGroupingFactory);

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

            var itemTypeGroupingFactory = ItemTypeGroupingFactory.Create();
            _itemTypeGroupingRepository = ItemTypeGroupingRepository.Create(
                database,
                itemTypeGroupingFactory);

            _itemNamePartFactory = Core.Items.Names.ItemNamePartFactory.Create();
            _itemNamePartRepository = ItemNamePartRepository.Create(
                database,
                _itemNamePartFactory);
            
            var itemNameAffixFactory = ItemNameAffixFactory.Create();
            _itemNameAffixRepository = ItemNameAffixRepository.Create(
                database,
                itemNameAffixFactory);
            _itemNameAffixFilter = Names.ItemNameAffixFilter.Create(
                database,
                itemNameAffixFactory);

            var itemTypeGeneratorPluginFactory = ItemTypeGeneratorPluginFactory.Create();
            _itemTypeGeneratorPluginRepository = ItemTypeGeneratorPluginRepository.Create(
                database,
                itemTypeGeneratorPluginFactory);

            var magicTypeFactory = MagicTypeFactory.Create();
            _magicTypeRepository = MagicTypeRepository.Create(
                database,
                magicTypeFactory);

            var magicTypeGroupingFactory = MagicTypeGroupingFactory.Create();
            _magicTypeGroupingRepository = MagicTypeGroupingRepository.Create(
                database,
                magicTypeGroupingFactory);

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

        public IItemAffixDefinitionMagicTypeGroupingRepository ItemAffixDefinitionMagicTypeGroupings { get { return _itemAffixDefinitionMagicTypeGroupingRepository; } }

        public IMagicTypesRandomAffixesRepository MagicTypesRandomAffixes { get { return _magicTypesRandomAffixesRepository; } }

        public ISocketTypeRepository SocketTypes { get { return _socketTypeRepository; } }

        public IMaterialTypeRepository MaterialTypes { get { return _materialTypeRepository; } }

        public IItemTypeRepository ItemTypes { get { return _itemTypeRepository; } }

        public IItemTypeGroupingRepository ItemTypeGroupings { get { return _itemTypeGroupingRepository; } }

        public IItemNamePartRepository ItemNameParts { get { return _itemNamePartRepository; } }

        public IItemNameAffixRepository ItemNameAffixes { get { return _itemNameAffixRepository; } }

        public IItemNameAffixFilter ItemNameAffixFilter { get { return _itemNameAffixFilter; } }

        public IItemNamePartFactory ItemNamePartFactory { get { return _itemNamePartFactory; } }

        public IItemTypeGeneratorPluginRepository ItemTypeGeneratorPlugins { get { return _itemTypeGeneratorPluginRepository; } }

        public IMagicTypeRepository MagicTypes { get { return _magicTypeRepository; } }

        public IMagicTypeGroupingRepository MagicTypeGroupings { get { return _magicTypeGroupingRepository; } }

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
