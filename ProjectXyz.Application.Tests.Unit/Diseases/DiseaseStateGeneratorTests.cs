using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Diseases;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Diseases;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Diseases;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Unit.Diseases
{
    [ApplicationLayer]
    [Diseases]
    public class DiseaseStateGeneratorTests
    {
        [Fact]
        public void DiseaseStateGenerator_GenerateForId_Success()
        {
            // Setup
            Guid diseaseStateNameResourceId = Guid.NewGuid();
            Guid diseaseStateId = Guid.NewGuid();
            Guid diseaseStateSpreadId = Guid.NewGuid();
            Guid diseaseStateEnchantmentsId = Guid.NewGuid();
            Guid diseaseStateNextId = Guid.NewGuid();
            Guid diseaseStatePreviousId = Guid.NewGuid();
            Guid enchantmentId = Guid.NewGuid();

            var expectedResult = new Mock<IDiseaseState>();

            var diseaseStateFactory = new Mock<IDiseaseStateFactory>();
            diseaseStateFactory
                .Setup(x => x.Create(
                    diseaseStateId, 
                    diseaseStateNameResourceId,
                    diseaseStatePreviousId,
                    diseaseStateNextId,
                    diseaseStateSpreadId,
                    It.IsAny<IEnumerable<IEnchantment>>()))
                .Callback<Guid, Guid, Guid, Guid, Guid, IEnumerable<IEnchantment>>((a, b, c, d, e, f) => f.ToList())
                .Returns(expectedResult.Object);

            var randomizer = new Mock<IRandom>();

            var enchantment = new Mock<IEnchantment>();

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>();
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, enchantmentId))
                .Returns(enchantment.Object);

            var diseaseStateDefinition = new Mock<IDiseaseStateDefinition>();
            diseaseStateDefinition
                .Setup(x => x.Id)
                .Returns(diseaseStateId);
            diseaseStateDefinition
                .Setup(x => x.NameStringResourceId)
                .Returns(diseaseStateNameResourceId);
            diseaseStateDefinition
                .Setup(x => x.DiseaseSpreadTypeId)
                .Returns(diseaseStateSpreadId);
            diseaseStateDefinition
                .Setup(x => x.NextStateId)
                .Returns(diseaseStateNextId);
            diseaseStateDefinition
                .Setup(x => x.PreviousStateId)
                .Returns(diseaseStatePreviousId);

            var diseaseStateDefinitionRepository = new Mock<IDiseaseStateDefinitionRepository>();
            diseaseStateDefinitionRepository
                .Setup(x => x.GetById(diseaseStateId))
                .Returns(diseaseStateDefinition.Object);

            var diseaseStatesEnchantments = new Mock<IDiseaseStatesEnchantments>();
            diseaseStatesEnchantments
                .Setup(x => x.EnchantmentIds)
                .Returns(new[] { enchantmentId });

            var diseaseStateEnchantmentsRepository = new Mock<IDiseaseStatesEnchantmentsRepository>();
            diseaseStateEnchantmentsRepository
                .Setup(x => x.GetByDiseaseStateId(diseaseStateId))
                .Returns(diseaseStatesEnchantments.Object);

            var diseaseGenerator = DiseaseStateGenerator.Create(
                diseaseStateFactory.Object,
                enchantmentGenerator.Object,
                diseaseStateDefinitionRepository.Object,
                diseaseStateEnchantmentsRepository.Object);

            // Execute
            var result = diseaseGenerator.GenerateForId(
                randomizer.Object, 
                diseaseStateId);
            
            // Assert
            Assert.Equal(expectedResult.Object, result);

            diseaseStateDefinitionRepository.Verify(x => x.GetById(diseaseStateId), Times.Once);

            diseaseStateEnchantmentsRepository.Verify(x => x.GetByDiseaseStateId(It.IsAny<Guid>()), Times.Once);

            diseaseStatesEnchantments.Verify(x => x.EnchantmentIds, Times.Once);            

            enchantmentGenerator.Verify(x => x.Generate(It.IsAny<IRandom>(), It.IsAny<Guid>()), Times.Once);

            diseaseStateFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<IEnumerable<IEnchantment>>()),
                Times.Once);

            diseaseStateDefinition.Verify(x => x.Id, Times.Once);
            diseaseStateDefinition.Verify(x => x.DiseaseSpreadTypeId, Times.Once);
            diseaseStateDefinition.Verify(x => x.Id, Times.Once);
            diseaseStateDefinition.Verify(x => x.NameStringResourceId, Times.Once);
            diseaseStateDefinition.Verify(x => x.NextStateId, Times.Once);
            diseaseStateDefinition.Verify(x => x.PreviousStateId, Times.Once);
        }
    }
}