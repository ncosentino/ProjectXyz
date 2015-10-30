using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Sockets;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Data.Interface.Items.Names;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Items.Sockets
{
    public sealed class SocketPatternItemConverter : ISocketPatternItemConverter
    {
        #region Fields
        private readonly IStatMerger _statMerger;
        private readonly IItemFactory _itemFactory;
        private readonly IItemMetaDataFactory _itemMetaDataFactory;
        private readonly IItemNamePartFactory _itemNamePartFactory;
        private readonly IStatGenerator _statGenerator;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly ISocketPatternDefinitionStatRepository _socketPatternDefinitionStatRepository;
        private readonly ISocketPatternDefinitionEnchantmentRepository _socketPatternDefinitionEnchantmentRepository;
        #endregion

        #region Constructors
        private SocketPatternItemConverter(
            IStatMerger statMerger,
            IItemFactory itemFactory, 
            IItemMetaDataFactory itemMetaDataFactory,
            IItemNamePartFactory itemNamePartFactory, 
            IStatGenerator statGenerator,
            IEnchantmentGenerator enchantmentGenerator, 
            ISocketPatternDefinitionStatRepository socketPatternDefinitionStatRepository, 
            ISocketPatternDefinitionEnchantmentRepository socketPatternDefinitionEnchantmentRepository)
        {
            _statMerger = statMerger;
            _itemFactory = itemFactory;
            _itemMetaDataFactory = itemMetaDataFactory;
            _itemNamePartFactory = itemNamePartFactory;
            _statGenerator = statGenerator;
            _enchantmentGenerator = enchantmentGenerator;
            _socketPatternDefinitionStatRepository = socketPatternDefinitionStatRepository;
            _socketPatternDefinitionEnchantmentRepository = socketPatternDefinitionEnchantmentRepository;
        }
        #endregion

        #region Methods
        public static ISocketPatternItemConverter Create(
            IStatMerger statMerger,
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemNamePartFactory itemNamePartFactory,
            IStatGenerator statGenerator,
            IEnchantmentGenerator enchantmentGenerator,
            ISocketPatternDefinitionStatRepository socketPatternDefinitionStatRepository,
            ISocketPatternDefinitionEnchantmentRepository socketPatternDefinitionEnchantmentRepository)
        {
            var converter = new SocketPatternItemConverter(
                statMerger,
                itemFactory,
                itemMetaDataFactory,
                itemNamePartFactory,
                statGenerator,
                enchantmentGenerator,
                socketPatternDefinitionStatRepository,
                socketPatternDefinitionEnchantmentRepository);
            return converter;
        }

        public IItem ConvertItem(
            IItemContext itemContext,
            IRandom randomizer,
            IItem itemToConvert,
            IEnumerable<ISocketPatternDefinition> socketPatternDefinitions)
        {
            foreach (var socketPatternDefinition in socketPatternDefinitions)
            {
                if (randomizer.NextDouble() > socketPatternDefinition.Chance)
                {
                    continue;
                }

                return ConvertItem(
                    itemContext,
                    randomizer,
                    itemToConvert,
                    socketPatternDefinition);
            }
            
            return itemToConvert;
        }

        public IItem ConvertItem(
            IItemContext itemContext,
            IRandom randomizer,
            IItem itemToConvert,
            ISocketPatternDefinition socketPatternDefinition)
        {
            var itemMetaData = _itemMetaDataFactory.Create(
                socketPatternDefinition.InventoryGraphicResourceId ?? itemToConvert.InventoryGraphicResourceId,
                socketPatternDefinition.MagicTypeId ?? itemToConvert.MagicTypeId,
                itemToConvert.ItemTypeId,
                itemToConvert.MaterialTypeId,
                itemToConvert.SocketTypeId);

            var itemNamePart = _itemNamePartFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                socketPatternDefinition.NameStringResourceId,
                0);

            var socketPatternDefinitionStats = _socketPatternDefinitionStatRepository.GetBySocketPatternId(socketPatternDefinition.Id);
            var socketPatternStats = _statGenerator.GenerateStats(
                randomizer,
                socketPatternDefinitionStats.Cast<IStatRange>());

            var stats = _statMerger.MergeStats(
                itemToConvert.BaseStats,
                socketPatternStats);

            var socketPatternEnchantmentDefinitions = _socketPatternDefinitionEnchantmentRepository.GetBySocketPatternId(socketPatternDefinition.Id);
            var socketPatternEnchantments = socketPatternEnchantmentDefinitions.Select(x => _enchantmentGenerator.Generate(randomizer, x.EnchantmentDefinitionId));
            var enchantments = itemToConvert
                .Enchantments
                .Concat(socketPatternEnchantments);

            var socketPatternItem = _itemFactory.Create(
                itemContext,
                Guid.NewGuid(),
                itemToConvert.ItemDefinitionId,
                itemMetaData,
                new[] { itemNamePart },
                itemToConvert.Requirements,
                stats,
                enchantments,
                itemToConvert.Affixes,
                itemToConvert.SocketedItems,
                itemToConvert.EquippableSlotIds);
            return socketPatternItem;
        }
        #endregion
    }
}