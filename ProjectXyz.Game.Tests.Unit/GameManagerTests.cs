using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Maps;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Game.Core;
using Xunit;

namespace ProjectXyz.Game.Tests.Unit
{
    public class GameManagerTests
    {
        #region Methods
        [Fact]
        public void Create_NoPlugins_NotNull()
        {
            // Setup
            var database = new Mock<IDatabase>(MockBehavior.Strict);

            var statFactory = new Mock<IStatFactory>(MockBehavior.Strict);

            var statsDataManager = new Mock<IStatsDataManager>(MockBehavior.Strict);
            statsDataManager
                .Setup(x => x.StatFactory)
                .Returns(statFactory.Object);

            var actorStoreRepository = new Mock<IActorStoreRepository>(MockBehavior.Strict);

            var mapStoreRepository = new Mock<IMapStoreRepository>(MockBehavior.Strict);

            var enchantmentStoreFactory = new Mock<IEnchantmentStoreFactory>(MockBehavior.Strict);

            var enchantmentStoreRepository = new Mock<IEnchantmentStoreRepository>(MockBehavior.Strict);

            var enchantmentDefinitionRepository = new Mock<IEnchantmentDefinitionRepository>(MockBehavior.Strict);

            var enchantmentPluginRepository = new Mock<IEnchantmentPluginRepository>(MockBehavior.Strict);

            var enchantmentDataManager = new Mock<IEnchantmentsDataManager>(MockBehavior.Strict);
            enchantmentDataManager
                .Setup(x => x.EnchantmentStoreFactory)
                .Returns(enchantmentStoreFactory.Object);
            enchantmentDataManager
               .Setup(x => x.EnchantmentStores)
               .Returns(enchantmentStoreRepository.Object);
            enchantmentDataManager
               .Setup(x => x.EnchantmentDefinitions)
               .Returns(enchantmentDefinitionRepository.Object);
            enchantmentDataManager
               .Setup(x => x.EnchantmentPlugins)
               .Returns(enchantmentPluginRepository.Object);

            var itemAffixDefinitionRepository = new Mock<IItemAffixDefinitionRepository>(MockBehavior.Strict);

            var itemAffixDefinitionFilterRepository = new Mock<IItemAffixDefinitionFilterRepository>(MockBehavior.Strict);

            var itemAffixDefinitionEnchantmentRepository = new Mock<IItemAffixDefinitionEnchantmentRepository>(MockBehavior.Strict);

            var magicTypesRandomAffixesRepository = new Mock<IMagicTypesRandomAffixesRepository>(MockBehavior.Strict);

            var itemDataManager = new Mock<IItemDataManager>(MockBehavior.Strict);
            itemDataManager
                .Setup(x => x.ItemAffixDefinitions)
                .Returns(itemAffixDefinitionRepository.Object);
            itemDataManager
                .Setup(x => x.ItemAffixDefinitionFilter)
                .Returns(itemAffixDefinitionFilterRepository.Object);
            itemDataManager
                .Setup(x => x.ItemAffixDefinitionEnchantment)
                .Returns(itemAffixDefinitionEnchantmentRepository.Object);
            itemDataManager
                .Setup(x => x.MagicTypesRandomAffixes)
                .Returns(magicTypesRandomAffixesRepository.Object);

            var dataManager = new Mock<IDataManager>(MockBehavior.Strict);
            dataManager
                .Setup(x => x.Actors)
                .Returns(actorStoreRepository.Object);
            dataManager
                .Setup(x => x.Maps)
                .Returns(mapStoreRepository.Object);
            dataManager
                .Setup(x => x.Enchantments)
                .Returns(enchantmentDataManager.Object);
            dataManager
                .Setup(x => x.Items)
                .Returns(itemDataManager.Object);
            dataManager
                .Setup(x => x.Stats)
                .Returns(statsDataManager.Object);

            // Execute
            var result = GameManager.Create(
                database.Object,
                dataManager.Object,
                Enumerable.Empty<string>());

            // Assert
            Assert.NotNull(result);

            statsDataManager.Verify(x => x.StatFactory, Times.Once);

            enchantmentDataManager.Verify(x => x.EnchantmentStoreFactory, Times.Exactly(2));
            enchantmentDataManager.Verify(x => x.EnchantmentStores, Times.Exactly(2));
            enchantmentDataManager.Verify(x => x.EnchantmentDefinitions, Times.Exactly(2));

            itemDataManager.Verify(x => x.ItemAffixDefinitions, Times.Once);
            itemDataManager.Verify(x => x.ItemAffixDefinitionFilter, Times.Once);
            itemDataManager.Verify(x => x.ItemAffixDefinitionEnchantment, Times.Once);
            itemDataManager.Verify(x => x.MagicTypesRandomAffixes, Times.Once);

            dataManager.Verify(x => x.Actors, Times.Once);
            dataManager.Verify(x => x.Maps, Times.Once);
            dataManager.Verify(x => x.Enchantments, Times.Exactly(8));
            dataManager.Verify(x => x.Items, Times.Exactly(4));
            dataManager.Verify(x => x.Stats, Times.Once);
        }
        #endregion
    }
}
