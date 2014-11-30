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

namespace ProjectXyz.Application.Tests.Items
{
    [ApplicationLayer]
    [Items]
    public class RandomItemAffixGeneratorTests
    {
        #region Methods
        [Fact]
        public void RandomItemAffixGenerator_GenerateRandomEnchantments_ReturnsTwoEnchantments()
        {
            const int LEVEL = 0;
            Guid MAGIC_TYPE_ID = Guid.NewGuid();
            var RANDOMIZER = new Mock<IRandom>();
            Guid ENCHANTMENT_ID1 = Guid.NewGuid();
            Guid ENCHANTMENT_ID2 = Guid.NewGuid();

            var enchantment1 = new Mock<IEnchantment>();
            var enchantment2 = new Mock<IEnchantment>();

            var itemAffix = new Mock<INamedItemAffix>();
            itemAffix
                .Setup(x => x.Enchantments)
                .Returns(new IEnchantment[]
                {
                    enchantment1.Object,
                    enchantment2.Object,
                });

            var itemAffixGenerator = new Mock<IItemAffixGenerator>();
            itemAffixGenerator
                .Setup(x => x.GenerateRandom(RANDOMIZER.Object, LEVEL, MAGIC_TYPE_ID))
                .Returns(new INamedItemAffix[] 
                { 
                    itemAffix.Object
                });

            var affixGenerator = RandomItemAffixGenerator.Create(itemAffixGenerator.Object);

            var result = new List<IEnchantment>(affixGenerator.GenerateRandomEnchantments(RANDOMIZER.Object, LEVEL, MAGIC_TYPE_ID));

            Assert.Equal(2, result.Count);
            Assert.Equal(enchantment1.Object, result[0]);
            Assert.Equal(enchantment2.Object, result[result.Count - 1]);

            itemAffixGenerator.Verify(x => x.GenerateRandom(RANDOMIZER.Object, LEVEL, MAGIC_TYPE_ID), Times.Once);
        }
        #endregion
    }
}