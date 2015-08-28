using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Integration.Items
{
    [ApplicationLayer]
    [Items]
    public class NamedItemAffixGeneratorTests
    {
        #region Methods
        [Fact]
        public void NamedItemAffixGenerator_GenerateRandomNamed_TwoEnchantmentsPrefixAndSuffix()
        {
            const int LEVEL = 0;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();

            var randomizer = new Mock<IRandom>();
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(1)
                .Returns(1);

            var enchantment1 = new Mock<IEnchantment>();
            var enchantment2 = new Mock<IEnchantment>();

            var itemPrefix = new Mock<INamedItemAffix>();
            itemPrefix
                .Setup(x => x.Enchantments)
                .Returns(new IEnchantment[]
                {
                    enchantment1.Object
                });
            itemPrefix
                .Setup(x => x.NameStringResourceId)
                .Returns(Guid.NewGuid());

            var itemSuffix = new Mock<INamedItemAffix>();
            itemSuffix
                .Setup(x => x.Enchantments)
                .Returns(new IEnchantment[]
                {
                    enchantment2.Object
                });
            itemSuffix
                .Setup(x => x.NameStringResourceId)
                .Returns(Guid.NewGuid());

            var itemAffixGenerator = new Mock<IItemAffixGenerator>();
            itemAffixGenerator
                .Setup(x => x.GeneratePrefix(randomizer.Object, LEVEL, MAGIC_TYPE_ID))
                .Returns(itemPrefix.Object);
            itemAffixGenerator
                .Setup(x => x.GenerateSuffix(randomizer.Object, LEVEL, MAGIC_TYPE_ID))
                .Returns(itemSuffix.Object);

            var affixGenerator = NamedItemAffixGenerator.Create(itemAffixGenerator.Object);

            var result = affixGenerator.GenerateRandomNamedAffixes(randomizer.Object, LEVEL, MAGIC_TYPE_ID);

            Assert.Equal(itemPrefix.Object, result.Prefix);
            
            var enchantments = new List<IEnchantment>(result.Prefix.Enchantments);
            Assert.Equal(1, enchantments.Count);
            Assert.Equal(enchantment1.Object, enchantments[0]);

            Assert.Equal(itemSuffix.Object, result.Suffix);

            enchantments = new List<IEnchantment>(result.Suffix.Enchantments);
            Assert.Equal(1, enchantments.Count);
            Assert.Equal(enchantment2.Object, enchantments[0]);

            itemAffixGenerator.Verify(x => x.GeneratePrefix(randomizer.Object, LEVEL, MAGIC_TYPE_ID), Times.Once);
            itemAffixGenerator.Verify(x => x.GenerateSuffix(randomizer.Object, LEVEL, MAGIC_TYPE_ID), Times.Once);
        }

        [Fact]
        public void NamedItemAffixGenerator_GenerateRandomNamed_SingleEnchantmentOnlyPrefix()
        {
            const int LEVEL = 0;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();

            var randomizer = new Mock<IRandom>();
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(1)
                .Returns(0);

            var enchantment = new Mock<IEnchantment>();

            var itemPrefix = new Mock<INamedItemAffix>();
            itemPrefix
                .Setup(x => x.Enchantments)
                .Returns(new IEnchantment[]
                {
                    enchantment.Object
                });
            itemPrefix
                .Setup(x => x.NameStringResourceId)
                .Returns(Guid.NewGuid());

            var itemAffixGenerator = new Mock<IItemAffixGenerator>();
            itemAffixGenerator
                .Setup(x => x.GeneratePrefix(randomizer.Object, LEVEL, MAGIC_TYPE_ID))
                .Returns(itemPrefix.Object);
            
            var affixGenerator = NamedItemAffixGenerator.Create(itemAffixGenerator.Object);

            var result = affixGenerator.GenerateRandomNamedAffixes(randomizer.Object, LEVEL, MAGIC_TYPE_ID);

            Assert.NotNull(result.Prefix);
            Assert.Equal(1, result.Prefix.Enchantments.Count());
            Assert.Equal(enchantment.Object, result.Prefix.Enchantments.First());
            Assert.Equal(itemPrefix.Object, result.Prefix);

            Assert.Null(result.Suffix);
            
            itemAffixGenerator.Verify(x => x.GeneratePrefix(randomizer.Object, LEVEL, MAGIC_TYPE_ID), Times.Once);
            itemAffixGenerator.Verify(x => x.GenerateSuffix(randomizer.Object, LEVEL, MAGIC_TYPE_ID), Times.Never);
        }

        [Fact]
        public void NamedItemAffixGenerator_GenerateRandomNamed_SingleEnchantmentOnlySuffix()
        {
            const int LEVEL = 0;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();

            var randomizer = new Mock<IRandom>();
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(0)
                .Returns(0);

            var enchantment = new Mock<IEnchantment>();

            var itemSuffix = new Mock<INamedItemAffix>();
            itemSuffix
                .Setup(x => x.Enchantments)
                .Returns(new IEnchantment[]
                {
                    enchantment.Object
                });
            itemSuffix
                .Setup(x => x.NameStringResourceId)
                .Returns(Guid.NewGuid());

            var itemAffixGenerator = new Mock<IItemAffixGenerator>();
            itemAffixGenerator
                .Setup(x => x.GenerateSuffix(randomizer.Object, LEVEL, MAGIC_TYPE_ID))
                .Returns(itemSuffix.Object);

            var affixGenerator = NamedItemAffixGenerator.Create(itemAffixGenerator.Object);

            var result = affixGenerator.GenerateRandomNamedAffixes(randomizer.Object, LEVEL, MAGIC_TYPE_ID);

            Assert.Null(result.Prefix);
            
            Assert.NotNull(result.Suffix);
            Assert.Equal(1, result.Suffix.Enchantments.Count());
            Assert.Equal(enchantment.Object, result.Suffix.Enchantments.First());
            Assert.Equal(itemSuffix.Object, result.Suffix);

            itemAffixGenerator.Verify(x => x.GeneratePrefix(randomizer.Object, LEVEL, MAGIC_TYPE_ID), Times.Never);
            itemAffixGenerator.Verify(x => x.GenerateSuffix(randomizer.Object, LEVEL, MAGIC_TYPE_ID), Times.Once);
        }
        #endregion
    }
}