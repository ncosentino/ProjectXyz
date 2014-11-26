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
    public class RandomItemAffixGeneratorTests
    {
        #region Methods
        [Fact]
        public void RandomItemAffixGenerator_GenerateRandomEnchantments_ReturnsTwoEnchantments()
        {
            const int LEVEL = 0;
            const int MIN = 1;
            const int MAX = 1;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();
            var RANDOMIZER = new Mock<IRandom>();
            Guid AFFIX_ENCHANTMENTS_ID = new Guid();
            Guid ENCHANTMENT_ID1 = Guid.NewGuid();
            Guid ENCHANTMENT_ID2 = Guid.NewGuid();

            var itemAffix = new Mock<IItemAffix>();
            itemAffix
                .Setup(x => x.AffixEnchantmentsId)
                .Returns(AFFIX_ENCHANTMENTS_ID); ;

            var itemAffixRepository = new Mock<IItemAffixRepository>();
            itemAffixRepository
                .Setup(x => x.GenerateRandom(MIN, MAX, MAGIC_TYPE_ID, LEVEL, RANDOMIZER.Object))
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
                .Setup(x => x.Generate(ENCHANTMENT_ID1, RANDOMIZER.Object))
                .Returns(enchantment1.Object);
            enchantmentRepository
                .Setup(x => x.Generate(ENCHANTMENT_ID2, RANDOMIZER.Object))
                .Returns(enchantment2.Object);

            var magicTypesRandomAffixes = new Mock<IMagicTypesRandomAffixes>();
            magicTypesRandomAffixes
                .Setup(x => x.MinimumAffixes)
                .Returns(1);
            magicTypesRandomAffixes
                .Setup(x => x.MaximumAffixes)
                .Returns(1);

            var magicTypesRandomAffixesRepository = new Mock<IMagicTypesRandomAffixesRepository>();
            magicTypesRandomAffixesRepository
                .Setup(x => x.GetForMagicTypeId(MAGIC_TYPE_ID))
                .Returns(magicTypesRandomAffixes.Object);

            var affixGenerator = RandomItemAffixGenerator.Create(
                itemAffixRepository.Object,
                affixEnchantmentsRepository.Object,
                enchantmentRepository.Object,
                magicTypesRandomAffixesRepository.Object);

            var result = new List<IEnchantment>(affixGenerator.GenerateRandomEnchantments(MAGIC_TYPE_ID, LEVEL, RANDOMIZER.Object));

            Assert.Equal(2, result.Count);
            Assert.Equal(enchantment1.Object, result[0]);
            Assert.Equal(enchantment2.Object, result[result.Count - 1]);

            affixEnchantmentsRepository.Verify(x => x.GetForId(AFFIX_ENCHANTMENTS_ID), Times.Once);

            enchantmentRepository.Verify(x => x.Generate(ENCHANTMENT_ID1, RANDOMIZER.Object), Times.Once);
            enchantmentRepository.Verify(x => x.Generate(ENCHANTMENT_ID2, RANDOMIZER.Object), Times.Once);

            itemAffixRepository.Verify(x => x.GenerateRandom(MIN, MAX, MAGIC_TYPE_ID, LEVEL, RANDOMIZER.Object), Times.Once);

            magicTypesRandomAffixesRepository.Verify(x => x.GetForMagicTypeId(MAGIC_TYPE_ID), Times.Once);
        }
        #endregion
    }
}