using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Affixes;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Unit.Items.Affixes
{
    [ApplicationLayer]
    [Items]
    public class ItemAffixGeneratorTests
    {
        #region Methods
        [Fact]
        public void GenerateRandom_TwoAffixDefinitions_TwoAffixes()
        {
            // Setup
            const int NUMBER_OF_AFFIXESS = 2;
            const int LEVEL = 0;
            var magicTypeId = Guid.NewGuid();
            var itemAffixDefinitionId1 = Guid.NewGuid();
            var itemAffixDefinitionId2 = Guid.NewGuid();
            var enchantmentId1 = Guid.NewGuid();
            var enchantmentId2 = Guid.NewGuid();

            var enchantment1 = new Mock<IEnchantment>(MockBehavior.Strict);
            var enchantment2 = new Mock<IEnchantment>(MockBehavior.Strict);

            var randomizer = new Mock<IRandom>(MockBehavior.Strict);
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(0)
                .Returns(0)
                .Returns(0);

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>(MockBehavior.Strict);
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, enchantmentId1))
                .Returns(enchantment1.Object);
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, enchantmentId2))
                .Returns(enchantment2.Object);

            var itemAffixDefinition1 = new Mock<IItemAffixDefinition>(MockBehavior.Strict);
            itemAffixDefinition1
                .Setup(x => x.Id)
                .Returns(itemAffixDefinitionId1);

            var itemAffixDefinition2 = new Mock<IItemAffixDefinition>(MockBehavior.Strict);
            itemAffixDefinition2
                .Setup(x => x.Id)
                .Returns(itemAffixDefinitionId2);

            var itemAffixDefinitionRepository = new Mock<IItemAffixDefinitionRepository>(MockBehavior.Strict);
            itemAffixDefinitionRepository
                .Setup(x => x.GetById(itemAffixDefinitionId1))
                .Returns(itemAffixDefinition1.Object);
            itemAffixDefinitionRepository
                .Setup(x => x.GetById(itemAffixDefinitionId2))
                .Returns(itemAffixDefinition2.Object);

            var itemAffixDefinitionFilterRepository = new Mock<IItemAffixDefinitionFilterRepository>(MockBehavior.Strict);
            itemAffixDefinitionFilterRepository
                .Setup(x => x.GetIdsForFilter(
                    LEVEL,
                    int.MaxValue,
                    magicTypeId,
                    true,
                    true))
                .Returns(new[] { itemAffixDefinitionId1, itemAffixDefinitionId2 });

            var itemAffixEnchantment1 = new Mock<IItemAffixEnchantment>(MockBehavior.Strict);
            itemAffixEnchantment1
                .Setup(x => x.EnchantmentId)
                .Returns(enchantmentId1);

            var itemAffixEnchantment2 = new Mock<IItemAffixEnchantment>(MockBehavior.Strict);
            itemAffixEnchantment2
                .Setup(x => x.EnchantmentId)
                .Returns(enchantmentId2);

            var itemAffixEnchantmentRepository = new Mock<IItemAffixEnchantmentRepository>(MockBehavior.Strict);
            itemAffixEnchantmentRepository
                .Setup(x => x.GetByItemAffixDefinitionId(itemAffixDefinitionId1))
                .Returns(new[]
                {
                    itemAffixEnchantment1.Object
                });
            itemAffixEnchantmentRepository
                .Setup(x => x.GetByItemAffixDefinitionId(itemAffixDefinitionId2))
                .Returns(new[]
                {
                    itemAffixEnchantment2.Object
                });

            var itemAffix1 = new Mock<IItemAffix>(MockBehavior.Strict);
            var itemAffix2 = new Mock<IItemAffix>(MockBehavior.Strict);

            var itemAffixFactory = new Mock<IItemAffixFactory>(MockBehavior.Strict);
            itemAffixFactory
                .Setup(x => x.CreateItemAffix(new[]
                {
                    enchantment1.Object,
                }))
                .Returns(itemAffix1.Object);
            itemAffixFactory
                .Setup(x => x.CreateItemAffix(new[]
                {
                    enchantment2.Object,
                }))
                .Returns(itemAffix2.Object);

            var magicTypesRandomAffixes = new Mock<IMagicTypesRandomAffixes>(MockBehavior.Strict);
            magicTypesRandomAffixes
                .Setup(x => x.MinimumAffixes)
                .Returns(NUMBER_OF_AFFIXESS);
            magicTypesRandomAffixes
                .Setup(x => x.MaximumAffixes)
                .Returns(NUMBER_OF_AFFIXESS);

            var magicTypesRandomAffixesRepository = new Mock<IMagicTypesRandomAffixesRepository>(MockBehavior.Strict);
            magicTypesRandomAffixesRepository
                .Setup(x => x.GetForMagicTypeId(magicTypeId))
                .Returns(magicTypesRandomAffixes.Object);

            var itemAffixGenerator = ItemAffixGenerator.Create(
                enchantmentGenerator.Object,
                itemAffixDefinitionRepository.Object,
                itemAffixDefinitionFilterRepository.Object,
                itemAffixEnchantmentRepository.Object,
                itemAffixFactory.Object,
                magicTypesRandomAffixesRepository.Object);

            // Execute
            var result = itemAffixGenerator.GenerateRandom(
                randomizer.Object,
                LEVEL,
                magicTypeId).ToArray();

            // Assert
            Assert.Equal(2, result.Length);
            Assert.Equal(itemAffix1.Object, result.First());
            Assert.Equal(itemAffix2.Object, result.Last());

            randomizer.Verify(x => x.NextDouble(), Times.Exactly(3));

            enchantmentGenerator.Verify(x => x.Generate(It.IsAny<IRandom>(), It.IsAny<Guid>()), Times.Exactly(2));
            
            itemAffixDefinition1.Verify(x => x.Id, Times.Once);
            
            itemAffixDefinition2.Verify(x => x.Id, Times.Once);

            itemAffixDefinitionRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Exactly(2));

            itemAffixDefinitionFilterRepository.Verify(
                x => x.GetIdsForFilter(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Guid>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()),
                Times.Once);

            itemAffixEnchantment1.Verify(x => x.EnchantmentId, Times.Once);

            itemAffixEnchantment2.Verify(x => x.EnchantmentId, Times.Once);

            itemAffixEnchantmentRepository.Verify(x => x.GetByItemAffixDefinitionId(It.IsAny<Guid>()), Times.Exactly(2));
            
            itemAffixFactory.Verify(x => x.CreateItemAffix(It.IsAny<IEnumerable<IEnchantment>>()), Times.Exactly(2));

            magicTypesRandomAffixes.Verify(x => x.MinimumAffixes, Times.Once);
            magicTypesRandomAffixes.Verify(x => x.MaximumAffixes, Times.Once);

            magicTypesRandomAffixesRepository.Verify(x => x.GetForMagicTypeId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GeneratePrefix_DefinitionWithTwoEnchantments_PrefixWithTwoEnchantments()
        {
            // Setup
            const int LEVEL = 0;
            var magicTypeId = Guid.NewGuid();
            var itemPrefixId = Guid.NewGuid();
            var enchantmentId1 = Guid.NewGuid();
            var enchantmentId2 = Guid.NewGuid();
            var affixNameStringResourceId = Guid.NewGuid();

            var enchantment1 = new Mock<IEnchantment>(MockBehavior.Strict);
            var enchantment2 = new Mock<IEnchantment>(MockBehavior.Strict);

            var randomizer = new Mock<IRandom>(MockBehavior.Strict);
            randomizer
                .Setup(x => x.NextDouble())
                .Returns(0);

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>(MockBehavior.Strict);
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, enchantmentId1))
                .Returns(enchantment1.Object);
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, enchantmentId2))
                .Returns(enchantment2.Object);

            var itemPrefixDefinition = new Mock<IItemAffixDefinition>(MockBehavior.Strict);
            itemPrefixDefinition
                .Setup(x => x.Id)
                .Returns(itemPrefixId);
            itemPrefixDefinition
                .Setup(x => x.NameStringResourceId)
                .Returns(affixNameStringResourceId);
            
            var itemAffixDefinitionRepository = new Mock<IItemAffixDefinitionRepository>(MockBehavior.Strict);
            itemAffixDefinitionRepository
                .Setup(x => x.GetById(itemPrefixId))
                .Returns(itemPrefixDefinition.Object);

            var itemAffixDefinitionFilterRepository = new Mock<IItemAffixDefinitionFilterRepository>(MockBehavior.Strict);
            itemAffixDefinitionFilterRepository
                .Setup(x => x.GetIdsForFilter(
                    LEVEL,
                    int.MaxValue,
                    magicTypeId,
                    true,
                    false))
                .Returns(new[] { itemPrefixId });

            var itemAffixEnchantment1 = new Mock<IItemAffixEnchantment>(MockBehavior.Strict);
            itemAffixEnchantment1
                .Setup(x => x.EnchantmentId)
                .Returns(enchantmentId1);

            var itemAffixEnchantment2 = new Mock<IItemAffixEnchantment>(MockBehavior.Strict);
            itemAffixEnchantment2
                .Setup(x => x.EnchantmentId)
                .Returns(enchantmentId2);

            var itemAffixEnchantmentRepository = new Mock<IItemAffixEnchantmentRepository>(MockBehavior.Strict);
            itemAffixEnchantmentRepository
                .Setup(x => x.GetByItemAffixDefinitionId(itemPrefixId))
                .Returns(new[]
                {
                    itemAffixEnchantment1.Object,
                    itemAffixEnchantment2.Object
                });

            var itemPrefix = new Mock<INamedItemAffix>(MockBehavior.Strict);

            var itemAffixFactory = new Mock<IItemAffixFactory>(MockBehavior.Strict);
            itemAffixFactory
                .Setup(x => x.CreateNamedItemAffix(
                    new[]
                    {
                        enchantment1.Object,
                        enchantment2.Object
                    },
                    affixNameStringResourceId))
                .Returns(itemPrefix.Object);
            
            var magicTypesRandomAffixesRepository = new Mock<IMagicTypesRandomAffixesRepository>(MockBehavior.Strict);

            var itemAffixGenerator = ItemAffixGenerator.Create(
                enchantmentGenerator.Object,
                itemAffixDefinitionRepository.Object,
                itemAffixDefinitionFilterRepository.Object,
                itemAffixEnchantmentRepository.Object,
                itemAffixFactory.Object,
                magicTypesRandomAffixesRepository.Object);

            // Execute
            var result = itemAffixGenerator.GeneratePrefix(
                randomizer.Object,
                LEVEL,
                magicTypeId);

            // Assert
            Assert.Equal(itemPrefix.Object, result);

            randomizer.Verify(x => x.NextDouble(), Times.Once);

            enchantmentGenerator.Verify(x => x.Generate(It.IsAny<IRandom>(), It.IsAny<Guid>()), Times.Exactly(2));

            itemPrefixDefinition.Verify(x => x.Id, Times.Once);
            itemPrefixDefinition.Verify(x => x.NameStringResourceId, Times.Once);

            itemAffixDefinitionRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);

            itemAffixDefinitionFilterRepository.Verify(
                x => x.GetIdsForFilter(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Guid>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()),
                Times.Once);

            itemAffixEnchantment1.Verify(x => x.EnchantmentId, Times.Once);

            itemAffixEnchantment2.Verify(x => x.EnchantmentId, Times.Once);

            itemAffixEnchantmentRepository.Verify(x => x.GetByItemAffixDefinitionId(It.IsAny<Guid>()), Times.Once);

            itemAffixFactory.Verify(x => x.CreateNamedItemAffix(It.IsAny<IEnumerable<IEnchantment>>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GenerateSuffix_DefinitionWithTwoEnchantments_SuffixWithTwoEnchantments()
        {
            // Setup
            const int LEVEL = 0;
            var magicTypeId = Guid.NewGuid();
            var itemSuffixId = Guid.NewGuid();
            var enchantmentId1 = Guid.NewGuid();
            var enchantmentId2 = Guid.NewGuid();
            var affixNameStringResourceId = Guid.NewGuid();

            var enchantment1 = new Mock<IEnchantment>(MockBehavior.Strict);
            var enchantment2 = new Mock<IEnchantment>(MockBehavior.Strict);

            var randomizer = new Mock<IRandom>(MockBehavior.Strict);
            randomizer
                .Setup(x => x.NextDouble())
                .Returns(0);

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>(MockBehavior.Strict);
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, enchantmentId1))
                .Returns(enchantment1.Object);
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, enchantmentId2))
                .Returns(enchantment2.Object);

            var itemSuffixDefinition = new Mock<IItemAffixDefinition>(MockBehavior.Strict);
            itemSuffixDefinition
                .Setup(x => x.Id)
                .Returns(itemSuffixId);
            itemSuffixDefinition
                .Setup(x => x.NameStringResourceId)
                .Returns(affixNameStringResourceId);

            var itemAffixDefinitionRepository = new Mock<IItemAffixDefinitionRepository>(MockBehavior.Strict);
            itemAffixDefinitionRepository
                .Setup(x => x.GetById(itemSuffixId))
                .Returns(itemSuffixDefinition.Object);

            var itemAffixDefinitionFilterRepository = new Mock<IItemAffixDefinitionFilterRepository>(MockBehavior.Strict);
            itemAffixDefinitionFilterRepository
                .Setup(x => x.GetIdsForFilter(
                    LEVEL,
                    int.MaxValue,
                    magicTypeId,
                    false,
                    true))
                .Returns(new[] { itemSuffixId });

            var itemAffixEnchantment1 = new Mock<IItemAffixEnchantment>(MockBehavior.Strict);
            itemAffixEnchantment1
                .Setup(x => x.EnchantmentId)
                .Returns(enchantmentId1);

            var itemAffixEnchantment2 = new Mock<IItemAffixEnchantment>(MockBehavior.Strict);
            itemAffixEnchantment2
                .Setup(x => x.EnchantmentId)
                .Returns(enchantmentId2);

            var itemAffixEnchantmentRepository = new Mock<IItemAffixEnchantmentRepository>(MockBehavior.Strict);
            itemAffixEnchantmentRepository
                .Setup(x => x.GetByItemAffixDefinitionId(itemSuffixId))
                .Returns(new[]
                {
                    itemAffixEnchantment1.Object,
                    itemAffixEnchantment2.Object
                });

            var itemSuffix = new Mock<INamedItemAffix>(MockBehavior.Strict);

            var itemAffixFactory = new Mock<IItemAffixFactory>(MockBehavior.Strict);
            itemAffixFactory
                .Setup(x => x.CreateNamedItemAffix(
                    new[]
                    {
                        enchantment1.Object,
                        enchantment2.Object
                    },
                    affixNameStringResourceId))
                .Returns(itemSuffix.Object);

            var magicTypesRandomAffixesRepository = new Mock<IMagicTypesRandomAffixesRepository>(MockBehavior.Strict);

            var itemAffixGenerator = ItemAffixGenerator.Create(
                enchantmentGenerator.Object,
                itemAffixDefinitionRepository.Object,
                itemAffixDefinitionFilterRepository.Object,
                itemAffixEnchantmentRepository.Object,
                itemAffixFactory.Object,
                magicTypesRandomAffixesRepository.Object);

            // Execute
            var result = itemAffixGenerator.GenerateSuffix(
                randomizer.Object,
                LEVEL,
                magicTypeId);

            // Assert
            Assert.Equal(itemSuffix.Object, result);

            randomizer.Verify(x => x.NextDouble(), Times.Once);

            enchantmentGenerator.Verify(x => x.Generate(It.IsAny<IRandom>(), It.IsAny<Guid>()), Times.Exactly(2));

            itemSuffixDefinition.Verify(x => x.Id, Times.Once);
            itemSuffixDefinition.Verify(x => x.NameStringResourceId, Times.Once);

            itemAffixDefinitionRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);

            itemAffixDefinitionFilterRepository.Verify(
                x => x.GetIdsForFilter(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Guid>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()),
                Times.Once);

            itemAffixEnchantment1.Verify(x => x.EnchantmentId, Times.Once);

            itemAffixEnchantment2.Verify(x => x.EnchantmentId, Times.Once);

            itemAffixEnchantmentRepository.Verify(x => x.GetByItemAffixDefinitionId(It.IsAny<Guid>()), Times.Once);

            itemAffixFactory.Verify(x => x.CreateNamedItemAffix(It.IsAny<IEnumerable<IEnchantment>>(), It.IsAny<Guid>()), Times.Once);
        }
        #endregion
    }
}