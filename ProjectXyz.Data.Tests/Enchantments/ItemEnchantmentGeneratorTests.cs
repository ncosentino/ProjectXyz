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

namespace ProjectXyz.Data.Tests.Enchantments
{
    [DataLayer]
    [Enchantments]
    public class ItemEnchantmentGeneratorTests
    {
        #region Methods
        [Fact]
        public void ItemEnchantmentGenerator_GenerateRandom1_ReturnsTwoEnchantments()
        {
            const int LEVEL = 0;
            const int MIN = 1;
            const int MAX = 1;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();
            Random RANDOMIZER = new Random(1);
            Guid AFFIX_ENCHANTMENTS_ID = new Guid();
            Guid ENCHANTMENT_ID1 = Guid.NewGuid();
            Guid ENCHANTMENT_ID2 = Guid.NewGuid();

            var itemAffix = new Mock<IItemAffix>();
            itemAffix
                .Setup(x => x.AffixEnchantmentsId)
                .Returns(AFFIX_ENCHANTMENTS_ID); ;

            var itemAffixRepository = new Mock<IItemAffixRepository>();
            itemAffixRepository
                .Setup(x => x.GenerateRandom(MIN, MAX, MAGIC_TYPE_ID, LEVEL, RANDOMIZER))
                .Returns(new IItemAffix[] 
                { 
                    itemAffix.Object
                });

            var affixEnchantments = new Mock<IAffixEnchantments>();
            affixEnchantments
                .Setup(x => x.EnchantmentIds)
                .Returns(new Guid[]
                {
                    ENCHANTMENT_ID1,
                    ENCHANTMENT_ID2,
                });

            var affixEnchantmentsRepository = new Mock<IAffixEnchantmentsRepository>();
            affixEnchantmentsRepository
                .Setup(x => x.GetForId(AFFIX_ENCHANTMENTS_ID))
                .Returns(affixEnchantments.Object);

            var enchantment1 = new Mock<IEnchantment>();
            var enchantment2 = new Mock<IEnchantment>();

            var enchantmentRepository = new Mock<IEnchantmentRepository>();
            enchantmentRepository
                .Setup(x => x.Generate(ENCHANTMENT_ID1, RANDOMIZER))
                .Returns(enchantment1.Object);
            enchantmentRepository
                .Setup(x => x.Generate(ENCHANTMENT_ID2, RANDOMIZER))
                .Returns(enchantment2.Object);

            var itemEnchantmentGenerator = ItemEnchantmentGenerator.Create(
                itemAffixRepository.Object,
                affixEnchantmentsRepository.Object,
                enchantmentRepository.Object);

            var result = new List<IEnchantment>(itemEnchantmentGenerator.GenerateRandom(MIN, MAX, MAGIC_TYPE_ID, LEVEL, RANDOMIZER));

            Assert.Equal(2, result.Count);
            Assert.Equal(enchantment1.Object, result[0]);
            Assert.Equal(enchantment2.Object, result[result.Count - 1]);

            affixEnchantmentsRepository.Verify(x => x.GetForId(AFFIX_ENCHANTMENTS_ID), Times.Once);
            
            enchantmentRepository.Verify(x => x.Generate(ENCHANTMENT_ID1, RANDOMIZER), Times.Once);
            enchantmentRepository.Verify(x => x.Generate(ENCHANTMENT_ID2, RANDOMIZER), Times.Once);

            itemAffixRepository.Verify(x => x.GenerateRandom(MIN, MAX, MAGIC_TYPE_ID, LEVEL, RANDOMIZER), Times.Once);
        }
        #endregion
    }
}