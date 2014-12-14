using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using Moq;

using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Application.Tests.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemAffixGeneratorTests
    {
        #region Methods
        [Fact]
        public void ItemAffixGenerator_GenerateRandomTwoAffixes_ReturnsTwoEnchantments()
        {
            const int LEVEL = 0;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();
            Guid ENCHANTMENT_ID1 = Guid.NewGuid();
            Guid ENCHANTMENT_ID2 = Guid.NewGuid();
            Guid AFFIX_CANDIDATE_ID1 = Guid.NewGuid();
            Guid AFFIX_CANDIDATE_ID2 = Guid.NewGuid();
            Guid AFFIX_ENCHANTMENT_ID1 = Guid.NewGuid();
            Guid AFFIX_ENCHANTMENT_ID2 = Guid.NewGuid();

            var randomizer = new Mock<IRandom>();
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(0);

            var enchantment1 = new Mock<IEnchantment>();
            var enchantment2 = new Mock<IEnchantment>();

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>();
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, ENCHANTMENT_ID1))
                .Callback(() =>
                {
                    
                })
                .Returns(enchantment1.Object);
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, ENCHANTMENT_ID2))
                .Returns(enchantment2.Object);

            var affixEnchantments1 = new Mock<IAffixEnchantments>();
            affixEnchantments1
                .Setup(x => x.EnchantmentIds)
                .Callback(() =>
                {

                })
                .Returns(new Guid[]
                {
                    ENCHANTMENT_ID1,
                });

            var affixEnchantments2 = new Mock<IAffixEnchantments>();
            affixEnchantments2
                .Setup(x => x.EnchantmentIds)
                .Returns(new Guid[]
                {
                    ENCHANTMENT_ID2,
                });

            var affixEnchantmentsRepository = new Mock<IAffixEnchantmentsRepository>();
            affixEnchantmentsRepository
                .Setup(x => x.GetById(AFFIX_ENCHANTMENT_ID1))
                .Returns(affixEnchantments1.Object);
            affixEnchantmentsRepository
                .Setup(x => x.GetById(AFFIX_ENCHANTMENT_ID2))
                .Returns(affixEnchantments2.Object);

            var itemAffixDefinition1 = new Mock<IItemAffixDefinition>();
            itemAffixDefinition1
                .Setup(x => x.AffixEnchantmentsId)
                .Returns(AFFIX_ENCHANTMENT_ID1);

            var itemAffixDefinition2 = new Mock<IItemAffixDefinition>();
            itemAffixDefinition2
                .Setup(x => x.AffixEnchantmentsId)
                .Returns(AFFIX_ENCHANTMENT_ID2);

            var itemAffixRepository = new Mock<IItemAffixDefinitionRepository>();
            itemAffixRepository
                .Setup(x => x.GetIdsForFilter(LEVEL, It.IsAny<int>(), MAGIC_TYPE_ID, true, true))
                .Returns(new Guid[]
                {
                    AFFIX_CANDIDATE_ID1,
                    AFFIX_CANDIDATE_ID2,
                });
            itemAffixRepository
                .Setup(x => x.GetById(AFFIX_CANDIDATE_ID1))
                .Returns(itemAffixDefinition1.Object);
            itemAffixRepository
                .Setup(x => x.GetById(AFFIX_CANDIDATE_ID2))
                .Returns(itemAffixDefinition2.Object);

            var affix1 = new Mock<IItemAffix>();
            var affix2 = new Mock<IItemAffix>();

            var itemAffixFactory = new Mock<IItemAffixFactory>();
            itemAffixFactory
                .Setup(
                    x =>
                        x.CreateItemAffix(
                            It.Is<IEnumerable<IEnchantment>>(ench => ench.Contains(enchantment1.Object))))
                .Returns(affix1.Object);

            itemAffixFactory
                .Setup(
                    x =>
                        x.CreateItemAffix(
                            It.Is<IEnumerable<IEnchantment>>(ench => ench.Contains(enchantment2.Object))))
                .Returns(affix2.Object);

            var magicTypesRandomAffixes = new Mock<IMagicTypesRandomAffixes>();
            magicTypesRandomAffixes
                .Setup(x => x.MinimumAffixes)
                .Returns(2);
            magicTypesRandomAffixes
                .Setup(x => x.MaximumAffixes)
                .Returns(2);

            var magicTypesRandomAffixesRepository = new Mock<IMagicTypesRandomAffixesRepository>();
            magicTypesRandomAffixesRepository
                .Setup(x => x.GetForMagicTypeId(MAGIC_TYPE_ID))
                .Returns(magicTypesRandomAffixes.Object);
                        
            var affixGenerator = ItemAffixGenerator.Create(
                enchantmentGenerator.Object,
                affixEnchantmentsRepository.Object,
                itemAffixRepository.Object,
                itemAffixFactory.Object,
                magicTypesRandomAffixesRepository.Object);

            var result = new List<IItemAffix>(affixGenerator.GenerateRandom(randomizer.Object, LEVEL, MAGIC_TYPE_ID));

            Assert.Equal(2, result.Count);

            Assert.Equal(affix1.Object, result[0]);
            Assert.Equal(affix2.Object, result[result.Count - 1]);

            magicTypesRandomAffixesRepository.Verify(x => x.GetForMagicTypeId(MAGIC_TYPE_ID), Times.Once);

            magicTypesRandomAffixes.Verify(x => x.MinimumAffixes, Times.AtLeastOnce());
            magicTypesRandomAffixes.Verify(x => x.MaximumAffixes, Times.AtLeastOnce());

            itemAffixRepository.Verify(x => x.GetIdsForFilter(LEVEL, It.IsAny<int>(), MAGIC_TYPE_ID, true, true), Times.Once);

            itemAffixRepository.Verify(x => x.GetById(AFFIX_CANDIDATE_ID1), Times.Once);
            itemAffixRepository.Verify(x => x.GetById(AFFIX_CANDIDATE_ID2), Times.Once);

            affixEnchantmentsRepository.Verify(x => x.GetById(AFFIX_ENCHANTMENT_ID1), Times.Once);
            affixEnchantmentsRepository.Verify(x => x.GetById(AFFIX_ENCHANTMENT_ID2), Times.Once);

            affixEnchantments1.Verify(x => x.EnchantmentIds, Times.Once);
            affixEnchantments2.Verify(x => x.EnchantmentIds, Times.Once);

            enchantmentGenerator.Verify(x => x.Generate(randomizer.Object, ENCHANTMENT_ID1), Times.Exactly(2)); // second enumeration is because of mock setup
            enchantmentGenerator.Verify(x => x.Generate(randomizer.Object, ENCHANTMENT_ID2), Times.Exactly(2)); // second enumeration is because of mock setup

            itemAffixFactory.Verify(x => x.CreateItemAffix(It.IsAny<IEnumerable<IEnchantment>>()), Times.Exactly(2));
        }

        public void ItemAffixGenerator_GeneratePrefix_ReturnsTwoEnchantments()
        {
            const int LEVEL = 0;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();
            Guid ENCHANTMENT_ID1 = Guid.NewGuid();
            Guid ENCHANTMENT_ID2 = Guid.NewGuid();
            Guid AFFIX_CANDIDATE_ID = Guid.NewGuid();
            Guid AFFIX_ENCHANTMENT_ID = Guid.NewGuid();

            var randomizer = new Mock<IRandom>();
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(0);

            var enchantment1 = new Mock<IEnchantment>();
            var enchantment2 = new Mock<IEnchantment>();

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>();
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, ENCHANTMENT_ID1))
                .Callback(() =>
                {

                })
                .Returns(enchantment1.Object);
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, ENCHANTMENT_ID2))
                .Returns(enchantment2.Object);

            var affixEnchantments1 = new Mock<IAffixEnchantments>();
            affixEnchantments1
                .Setup(x => x.EnchantmentIds)
                .Callback(() =>
                {

                })
                .Returns(new Guid[]
                {
                    ENCHANTMENT_ID1,
                    ENCHANTMENT_ID2
                });

            var affixEnchantmentsRepository = new Mock<IAffixEnchantmentsRepository>();
            affixEnchantmentsRepository
                .Setup(x => x.GetById(AFFIX_ENCHANTMENT_ID))
                .Returns(affixEnchantments1.Object);

            var itemAffixDefinition1 = new Mock<IItemAffixDefinition>();
            itemAffixDefinition1
                .Setup(x => x.AffixEnchantmentsId)
                .Returns(AFFIX_ENCHANTMENT_ID);

            var itemAffixRepository = new Mock<IItemAffixDefinitionRepository>();
            itemAffixRepository
                .Setup(x => x.GetIdsForFilter(LEVEL, It.IsAny<int>(), MAGIC_TYPE_ID, true, true))
                .Returns(new Guid[]
                {
                    AFFIX_CANDIDATE_ID,
                });
            itemAffixRepository
                .Setup(x => x.GetById(AFFIX_CANDIDATE_ID))
                .Returns(itemAffixDefinition1.Object);

            var affix1 = new Mock<IItemAffix>();

            var itemAffixFactory = new Mock<IItemAffixFactory>();
            itemAffixFactory
                .Setup(
                    x =>
                        x.CreateItemAffix(
                            It.Is<IEnumerable<IEnchantment>>(ench => ench.Contains(enchantment1.Object))))
                .Returns(affix1.Object);

            var magicTypesRandomAffixes = new Mock<IMagicTypesRandomAffixes>();

            var magicTypesRandomAffixesRepository = new Mock<IMagicTypesRandomAffixesRepository>();
            magicTypesRandomAffixesRepository
                .Setup(x => x.GetForMagicTypeId(MAGIC_TYPE_ID))
                .Returns(magicTypesRandomAffixes.Object);

            var affixGenerator = ItemAffixGenerator.Create(
                enchantmentGenerator.Object,
                affixEnchantmentsRepository.Object,
                itemAffixRepository.Object,
                itemAffixFactory.Object,
                magicTypesRandomAffixesRepository.Object);

            var result = affixGenerator.GeneratePrefix(randomizer.Object, LEVEL, MAGIC_TYPE_ID);

            Assert.Equal(affix1.Object, result);

            magicTypesRandomAffixesRepository.Verify(x => x.GetForMagicTypeId(MAGIC_TYPE_ID), Times.Once);

            magicTypesRandomAffixes.Verify(x => x.MinimumAffixes, Times.AtLeastOnce());
            magicTypesRandomAffixes.Verify(x => x.MaximumAffixes, Times.AtLeastOnce());

            itemAffixRepository.Verify(x => x.GetIdsForFilter(LEVEL, It.IsAny<int>(), MAGIC_TYPE_ID, true, true), Times.Once);

            itemAffixRepository.Verify(x => x.GetById(AFFIX_CANDIDATE_ID), Times.Once);

            affixEnchantmentsRepository.Verify(x => x.GetById(AFFIX_ENCHANTMENT_ID), Times.Once);

            affixEnchantments1.Verify(x => x.EnchantmentIds, Times.Once);

            enchantmentGenerator.Verify(x => x.Generate(randomizer.Object, ENCHANTMENT_ID1), Times.Exactly(2)); // second enumeration is because of mock setup
            enchantmentGenerator.Verify(x => x.Generate(randomizer.Object, ENCHANTMENT_ID2), Times.Exactly(2)); // second enumeration is because of mock setup

            itemAffixFactory.Verify(x => x.CreateItemAffix(It.IsAny<IEnumerable<IEnchantment>>()), Times.Exactly(2));
        }

        public void ItemAffixGenerator_GenerateSuffix_ReturnsTwoEnchantments()
        {
            const int LEVEL = 0;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();
            Guid ENCHANTMENT_ID1 = Guid.NewGuid();
            Guid ENCHANTMENT_ID2 = Guid.NewGuid();
            Guid AFFIX_CANDIDATE_ID = Guid.NewGuid();
            Guid AFFIX_ENCHANTMENT_ID = Guid.NewGuid();

            var randomizer = new Mock<IRandom>();
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(0);

            var enchantment1 = new Mock<IEnchantment>();
            var enchantment2 = new Mock<IEnchantment>();

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>();
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, ENCHANTMENT_ID1))
                .Returns(enchantment1.Object);
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, ENCHANTMENT_ID2))
                .Returns(enchantment2.Object);

            var affixEnchantments1 = new Mock<IAffixEnchantments>();
            affixEnchantments1
                .Setup(x => x.EnchantmentIds)
                .Returns(new Guid[]
                {
                    ENCHANTMENT_ID1,
                    ENCHANTMENT_ID2
                });

            var affixEnchantmentsRepository = new Mock<IAffixEnchantmentsRepository>();
            affixEnchantmentsRepository
                .Setup(x => x.GetById(AFFIX_ENCHANTMENT_ID))
                .Returns(affixEnchantments1.Object);

            var itemAffixDefinition1 = new Mock<IItemAffixDefinition>();
            itemAffixDefinition1
                .Setup(x => x.AffixEnchantmentsId)
                .Returns(AFFIX_ENCHANTMENT_ID);

            var itemAffixRepository = new Mock<IItemAffixDefinitionRepository>();
            itemAffixRepository
                .Setup(x => x.GetIdsForFilter(LEVEL, It.IsAny<int>(), MAGIC_TYPE_ID, true, true))
                .Returns(new Guid[]
                {
                    AFFIX_CANDIDATE_ID,
                });
            itemAffixRepository
                .Setup(x => x.GetById(AFFIX_CANDIDATE_ID))
                .Returns(itemAffixDefinition1.Object);

            var affix1 = new Mock<IItemAffix>();

            var itemAffixFactory = new Mock<IItemAffixFactory>();
            itemAffixFactory
                .Setup(
                    x =>
                        x.CreateItemAffix(
                            It.Is<IEnumerable<IEnchantment>>(ench => ench.Contains(enchantment1.Object))))
                .Returns(affix1.Object);

            var magicTypesRandomAffixes = new Mock<IMagicTypesRandomAffixes>();

            var magicTypesRandomAffixesRepository = new Mock<IMagicTypesRandomAffixesRepository>();
            magicTypesRandomAffixesRepository
                .Setup(x => x.GetForMagicTypeId(MAGIC_TYPE_ID))
                .Returns(magicTypesRandomAffixes.Object);

            var affixGenerator = ItemAffixGenerator.Create(
                enchantmentGenerator.Object,
                affixEnchantmentsRepository.Object,
                itemAffixRepository.Object,
                itemAffixFactory.Object,
                magicTypesRandomAffixesRepository.Object);

            var result = affixGenerator.GenerateSuffix(randomizer.Object, LEVEL, MAGIC_TYPE_ID);

            Assert.Equal(affix1.Object, result);

            magicTypesRandomAffixesRepository.Verify(x => x.GetForMagicTypeId(MAGIC_TYPE_ID), Times.Once);

            magicTypesRandomAffixes.Verify(x => x.MinimumAffixes, Times.AtLeastOnce());
            magicTypesRandomAffixes.Verify(x => x.MaximumAffixes, Times.AtLeastOnce());

            itemAffixRepository.Verify(x => x.GetIdsForFilter(LEVEL, It.IsAny<int>(), MAGIC_TYPE_ID, true, true), Times.Once);

            itemAffixRepository.Verify(x => x.GetById(AFFIX_CANDIDATE_ID), Times.Once);

            affixEnchantmentsRepository.Verify(x => x.GetById(AFFIX_ENCHANTMENT_ID), Times.Once);

            affixEnchantments1.Verify(x => x.EnchantmentIds, Times.Once);

            enchantmentGenerator.Verify(x => x.Generate(randomizer.Object, ENCHANTMENT_ID1), Times.Exactly(2)); // second enumeration is because of mock setup
            enchantmentGenerator.Verify(x => x.Generate(randomizer.Object, ENCHANTMENT_ID2), Times.Exactly(2)); // second enumeration is because of mock setup

            itemAffixFactory.Verify(x => x.CreateItemAffix(It.IsAny<IEnumerable<IEnchantment>>()), Times.Exactly(2));
        }
        #endregion
    }
}