using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using Moq;

using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Data.Tests.Enchantments
{
    [DataLayer]
    [Enchantments]
    public class NamedItemAffixGeneratorTests
    {
        #region Methods
        [Fact]
        public void NamedItemAffixGenerator_GenerateRandomNamed_TwoEnchantmentsPrefixAndSuffix()
        {
            const int LEVEL = 0;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();
            Guid AFFIX_ENCHANTMENTS_ID = new Guid();
            Guid ENCHANTMENT_ID1 = Guid.NewGuid();
            const string EXPECTED_PREFIX = "The Prefix";
            const string EXPECTED_SUFFIX = "The Suffix";

            var randomizer = new Mock<IRandom>();
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(1)
                .Returns(1);

            var itemPrefix = new Mock<IItemAffix>();
            itemPrefix
                .Setup(x => x.AffixEnchantmentsId)
                .Returns(AFFIX_ENCHANTMENTS_ID); ;
            itemPrefix
                .Setup(x => x.Name)
                .Returns(EXPECTED_PREFIX);
            itemPrefix
                .Setup(x => x.IsPrefix)
                .Returns(true);

            var itemSuffix = new Mock<IItemAffix>();
            itemSuffix
                .Setup(x => x.AffixEnchantmentsId)
                .Returns(AFFIX_ENCHANTMENTS_ID); ;
            itemSuffix
                .Setup(x => x.Name)
                .Returns(EXPECTED_SUFFIX);
            itemSuffix
                .Setup(x => x.IsPrefix)
                .Returns(false);

            var itemAffixRepository = new Mock<IItemAffixRepository>();
            itemAffixRepository
                .Setup(x => x.GenerateRandom(true, MAGIC_TYPE_ID, LEVEL, randomizer.Object))
                .Returns(itemPrefix.Object);
            itemAffixRepository
                .Setup(x => x.GenerateRandom(false, MAGIC_TYPE_ID, LEVEL, randomizer.Object))
                .Returns(itemSuffix.Object);

            var affixEnchantments = new Mock<IAffixEnchantments>();
            affixEnchantments
                .Setup(x => x.EnchantmentIds)
                .Returns(new Guid[]
                {
                    ENCHANTMENT_ID1,
                });

            var affixEnchantmentsRepository = new Mock<IAffixEnchantmentsRepository>();
            affixEnchantmentsRepository
                .Setup(x => x.GetForId(AFFIX_ENCHANTMENTS_ID))
                .Returns(affixEnchantments.Object);

            var enchantment1 = new Mock<IEnchantment>();

            var enchantmentRepository = new Mock<IEnchantmentRepository>();
            enchantmentRepository
                .Setup(x => x.Generate(ENCHANTMENT_ID1, randomizer.Object))
                .Returns(enchantment1.Object);

            var affixGenerator = NamedItemAffixGenerator.Create(
                itemAffixRepository.Object,
                affixEnchantmentsRepository.Object,
                enchantmentRepository.Object);

            string actualPrefix;
            string actualSuffix;
            var result = new List<IEnchantment>(affixGenerator.GenerateRandomNamed(MAGIC_TYPE_ID, LEVEL, randomizer.Object, out actualPrefix, out actualSuffix));

            Assert.Equal(2, result.Count);
            Assert.Equal(enchantment1.Object, result[0]);
            Assert.Equal(enchantment1.Object, result[result.Count - 1]);

            Assert.Equal(EXPECTED_PREFIX, actualPrefix);
            Assert.Equal(EXPECTED_SUFFIX, actualSuffix);

            randomizer.Verify(x => x.NextDouble(), Times.Exactly(2));

            affixEnchantmentsRepository.Verify(x => x.GetForId(AFFIX_ENCHANTMENTS_ID), Times.Exactly(2));

            enchantmentRepository.Verify(x => x.Generate(ENCHANTMENT_ID1, randomizer.Object), Times.Exactly(2));

            itemAffixRepository.Verify(x => x.GenerateRandom(true, MAGIC_TYPE_ID, LEVEL, randomizer.Object), Times.Once);
            itemAffixRepository.Verify(x => x.GenerateRandom(false, MAGIC_TYPE_ID, LEVEL, randomizer.Object), Times.Once);
        }

        [Fact]
        public void NamedItemAffixGenerator_GenerateRandomNamed_SingleEnchantmentOnlyPrefix()
        {
            const int LEVEL = 0;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();
            Guid AFFIX_ENCHANTMENTS_ID = new Guid();
            Guid ENCHANTMENT_ID1 = Guid.NewGuid();
            const string EXPECTED_PREFIX = "The Prefix";

            var randomizer = new Mock<IRandom>();
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(1)
                .Returns(0);

            var itemPrefix = new Mock<IItemAffix>();
            itemPrefix
                .Setup(x => x.AffixEnchantmentsId)
                .Returns(AFFIX_ENCHANTMENTS_ID); ;
            itemPrefix
                .Setup(x => x.Name)
                .Returns(EXPECTED_PREFIX);
            itemPrefix
                .Setup(x => x.IsPrefix)
                .Returns(true);

            var itemAffixRepository = new Mock<IItemAffixRepository>();
            itemAffixRepository
                .Setup(x => x.GenerateRandom(true, MAGIC_TYPE_ID, LEVEL, randomizer.Object))
                .Returns(itemPrefix.Object);

            var affixEnchantments = new Mock<IAffixEnchantments>();
            affixEnchantments
                .Setup(x => x.EnchantmentIds)
                .Returns(new Guid[]
                {
                    ENCHANTMENT_ID1,
                });

            var affixEnchantmentsRepository = new Mock<IAffixEnchantmentsRepository>();
            affixEnchantmentsRepository
                .Setup(x => x.GetForId(AFFIX_ENCHANTMENTS_ID))
                .Returns(affixEnchantments.Object);

            var enchantment1 = new Mock<IEnchantment>();

            var enchantmentRepository = new Mock<IEnchantmentRepository>();
            enchantmentRepository
                .Setup(x => x.Generate(ENCHANTMENT_ID1, randomizer.Object))
                .Returns(enchantment1.Object);

            var affixGenerator = NamedItemAffixGenerator.Create(
                itemAffixRepository.Object,
                affixEnchantmentsRepository.Object,
                enchantmentRepository.Object);

            string actualPrefix;
            string actualSuffix;
            var result = new List<IEnchantment>(affixGenerator.GenerateRandomNamed(MAGIC_TYPE_ID, LEVEL, randomizer.Object, out actualPrefix, out actualSuffix));

            Assert.Equal(1, result.Count);
            Assert.Equal(enchantment1.Object, result[result.Count - 1]);

            Assert.Null(actualSuffix);
            Assert.Equal(EXPECTED_PREFIX, actualPrefix);

            randomizer.Verify(x => x.NextDouble(), Times.Exactly(2));

            affixEnchantmentsRepository.Verify(x => x.GetForId(AFFIX_ENCHANTMENTS_ID), Times.Once());

            enchantmentRepository.Verify(x => x.Generate(ENCHANTMENT_ID1, randomizer.Object), Times.Once);

            itemAffixRepository.Verify(x => x.GenerateRandom(true, MAGIC_TYPE_ID, LEVEL, randomizer.Object), Times.Once);
            itemAffixRepository.Verify(x => x.GenerateRandom(false, MAGIC_TYPE_ID, LEVEL, randomizer.Object), Times.Never);
        }

        [Fact]
        public void NamedItemAffixGenerator_GenerateRandomNamed_SingleEnchantmentOnlySuffix()
        {
            const int LEVEL = 0;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();
            Guid AFFIX_ENCHANTMENTS_ID = new Guid();
            Guid ENCHANTMENT_ID1 = Guid.NewGuid();
            const string EXPECTED_SUFFIX = "The Suffix";

            var randomizer = new Mock<IRandom>();
            randomizer
                .Setup(x => x.NextDouble())
                .Returns(0);

            var itemSuffix = new Mock<IItemAffix>();
            itemSuffix
                .Setup(x => x.AffixEnchantmentsId)
                .Returns(AFFIX_ENCHANTMENTS_ID); ;
            itemSuffix
                .Setup(x => x.Name)
                .Returns(EXPECTED_SUFFIX);
            itemSuffix
                .Setup(x => x.IsPrefix)
                .Returns(false);

            var itemAffixRepository = new Mock<IItemAffixRepository>();
            itemAffixRepository
                .Setup(x => x.GenerateRandom(false, MAGIC_TYPE_ID, LEVEL, randomizer.Object))
                .Returns(itemSuffix.Object);

            var affixEnchantments = new Mock<IAffixEnchantments>();
            affixEnchantments
                .Setup(x => x.EnchantmentIds)
                .Returns(new Guid[]
                {
                    ENCHANTMENT_ID1,
                });

            var affixEnchantmentsRepository = new Mock<IAffixEnchantmentsRepository>();
            affixEnchantmentsRepository
                .Setup(x => x.GetForId(AFFIX_ENCHANTMENTS_ID))
                .Returns(affixEnchantments.Object);

            var enchantment1 = new Mock<IEnchantment>();

            var enchantmentRepository = new Mock<IEnchantmentRepository>();
            enchantmentRepository
                .Setup(x => x.Generate(ENCHANTMENT_ID1, randomizer.Object))
                .Returns(enchantment1.Object);

            var affixGenerator = NamedItemAffixGenerator.Create(
                itemAffixRepository.Object,
                affixEnchantmentsRepository.Object,
                enchantmentRepository.Object);

            string actualPrefix;
            string actualSuffix;
            var result = new List<IEnchantment>(affixGenerator.GenerateRandomNamed(MAGIC_TYPE_ID, LEVEL, randomizer.Object, out actualPrefix, out actualSuffix));

            Assert.Equal(1, result.Count);
            Assert.Equal(enchantment1.Object, result[result.Count - 1]);

            Assert.Null(actualPrefix);
            Assert.Equal(EXPECTED_SUFFIX, actualSuffix);

            randomizer.Verify(x => x.NextDouble(), Times.Once());

            affixEnchantmentsRepository.Verify(x => x.GetForId(AFFIX_ENCHANTMENTS_ID), Times.Once());

            enchantmentRepository.Verify(x => x.Generate(ENCHANTMENT_ID1, randomizer.Object), Times.Once);

            itemAffixRepository.Verify(x => x.GenerateRandom(true, MAGIC_TYPE_ID, LEVEL, randomizer.Object), Times.Never);
            itemAffixRepository.Verify(x => x.GenerateRandom(false, MAGIC_TYPE_ID, LEVEL, randomizer.Object), Times.Once);
        }
        #endregion
    }
}