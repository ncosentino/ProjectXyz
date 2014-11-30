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

namespace ProjectXyz.Application.Tests.Enchantments
{
    [ApplicationLayer]
    [Enchantments]
    public class EnchantmentGeneratorTests
    {
        #region Methods
        [Fact]
        public void EnchantmentGenerator_Generate_Success()
        {
            Guid ENCHANTMENT_ID = Guid.NewGuid();
            Guid STAT_ID = Guid.NewGuid();
            Guid CALCULATION_ID = Guid.NewGuid();
            Guid STATUS_TYPE_ID = Guid.NewGuid();
            Guid TRIGGER_ID = Guid.NewGuid();

            var randomizer = new Mock<IRandom>();
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(0.5)
                .Returns(0.5);

            var enchantment = new Mock<IEnchantment>();

            var enchantmentDefinition = new Mock<IEnchantmentDefinition>();
            enchantmentDefinition
                .Setup(x => x.CalculationId)
                .Returns(CALCULATION_ID);
            enchantmentDefinition
                .Setup(x => x.StatusTypeId)
                .Returns(STATUS_TYPE_ID);
            enchantmentDefinition
                .Setup(x => x.StatId)
                .Returns(STAT_ID);
            enchantmentDefinition
                .Setup(x => x.TriggerId)
                .Returns(TRIGGER_ID);
            enchantmentDefinition
                .Setup(x => x.MinimumValue)
                .Returns(0);
            enchantmentDefinition
                .Setup(x => x.MaximumValue)
                .Returns(100);
            enchantmentDefinition
                .Setup(x => x.MinimumDuration)
                .Returns(TimeSpan.FromSeconds(0));
            enchantmentDefinition
                .Setup(x => x.MaximumDuration)
                .Returns(TimeSpan.FromSeconds(2));

            var enchantmentDefinitionRepository = new Mock<IEnchantmentDefinitionRepository>();
            enchantmentDefinitionRepository
                .Setup(x => x.GetById(ENCHANTMENT_ID))
                .Returns(enchantmentDefinition.Object);

            var enchantmentFactory = new Mock<IEnchantmentFactory>();
            enchantmentFactory
                .Setup(x => x.Create(STAT_ID, STATUS_TYPE_ID, TRIGGER_ID, CALCULATION_ID, 50, TimeSpan.FromSeconds(1)))
                .Returns(enchantment.Object);

            var enchantmentGenerator = EnchantmentGenerator.Create(
                enchantmentDefinitionRepository.Object,
                enchantmentFactory.Object);

            var result = enchantmentGenerator.Generate(randomizer.Object, ENCHANTMENT_ID);

            Assert.Equal(enchantment.Object, result);

            enchantmentDefinitionRepository.Verify(x => x.GetById(ENCHANTMENT_ID), Times.Once);

            enchantmentFactory.Verify(x => x.Create(STAT_ID, STATUS_TYPE_ID, TRIGGER_ID, CALCULATION_ID, 50, TimeSpan.FromSeconds(1)), Times.Once);
        }
        #endregion
    }
}