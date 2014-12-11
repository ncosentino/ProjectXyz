using System;
using System.Linq;
using System.Collections.Generic;

using Xunit;

using Moq;

using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Interface.Diseases;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Application.Core.Diseases;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Diseases;
using ProjectXyz.Application.Interface;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Application.Tests.Diseases.Unit
{
    [ApplicationLayer]
    [Diseases]
    public class DiseaseStateGeneratorTests
    {
        [Fact]
        public void DiseaseStateGenerator_GenerateForId_Success()
        {
            const string DISEASE_STATE_NAME = "The Name";
            Guid DISEASE_STATE_ID = Guid.NewGuid();
            Guid DISEASE_STATE_SPREAD_ID = Guid.NewGuid();
            Guid DISEASE_STATE_ENCHANTMENTS_ID = Guid.NewGuid();
            Guid DISEASE_STATE_NEXT_ID = Guid.NewGuid();
            Guid DISEASE_STATE_PREVIOUS_ID = Guid.NewGuid();
            Guid ENCHANTMENT_ID = Guid.NewGuid();

            var expectedResult = new Mock<IDiseaseState>();

            var diseaseStateFactory = new Mock<IDiseaseStateFactory>();
            diseaseStateFactory
                .Setup(x => x.Create(
                    DISEASE_STATE_ID, 
                    DISEASE_STATE_NAME,
                    DISEASE_STATE_PREVIOUS_ID,
                    DISEASE_STATE_NEXT_ID,
                    DISEASE_STATE_SPREAD_ID,
                    It.IsAny<IEnumerable<IEnchantment>>()))
                .Callback<Guid, string, Guid, Guid, Guid, IEnumerable<IEnchantment>>((a, b, c, d, e, f) => f.ToList())
                .Returns(expectedResult.Object);

            var randomizer = new Mock<IRandom>();

            var enchantment = new Mock<IEnchantment>();

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>();
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, ENCHANTMENT_ID))
                .Returns(enchantment.Object);

            var diseaseStateDefinition = new Mock<IDiseaseStateDefinition>();
            diseaseStateDefinition
                .Setup(x => x.Id)
                .Returns(DISEASE_STATE_ID);
            diseaseStateDefinition
                .Setup(x => x.DiseaseStatesEnchantmentsId)
                .Returns(DISEASE_STATE_ENCHANTMENTS_ID);
            diseaseStateDefinition
                .Setup(x => x.Name)
                .Returns(DISEASE_STATE_NAME);
            diseaseStateDefinition
                .Setup(x => x.DiseaseSpreadTypeId)
                .Returns(DISEASE_STATE_SPREAD_ID);
            diseaseStateDefinition
                .Setup(x => x.NextStateId)
                .Returns(DISEASE_STATE_NEXT_ID);
            diseaseStateDefinition
                .Setup(x => x.PreviousStateId)
                .Returns(DISEASE_STATE_PREVIOUS_ID);

            var diseaseStateDefinitionRepository = new Mock<IDiseaseStateDefinitionRepository>();
            diseaseStateDefinitionRepository
                .Setup(x => x.GetById(DISEASE_STATE_ID))
                .Returns(diseaseStateDefinition.Object);

            var diseaseStatesEnchantments = new Mock<IDiseaseStatesEnchantments>();
            diseaseStatesEnchantments
                .Setup(x => x.EnchantmentIds)
                .Returns(new[] { ENCHANTMENT_ID });

            var diseaseStateEnchantmentsRepository = new Mock<IDiseaseStatesEnchantmentsRepository>();
            diseaseStateEnchantmentsRepository
                .Setup(x => x.GetById(DISEASE_STATE_ENCHANTMENTS_ID))
                .Returns(diseaseStatesEnchantments.Object);

            var diseaseGenerator = DiseaseStateGenerator.Create(
                diseaseStateFactory.Object,
                enchantmentGenerator.Object,
                diseaseStateDefinitionRepository.Object,
                diseaseStateEnchantmentsRepository.Object);

            var result = diseaseGenerator.GenerateForId(
                randomizer.Object, 
                DISEASE_STATE_ID);
            
            Assert.Equal(expectedResult.Object, result);

            diseaseStateDefinitionRepository
                .Verify(x => x.GetById(DISEASE_STATE_ID), Times.Once);

            diseaseStateEnchantmentsRepository
                .Verify(x => x.GetById(DISEASE_STATE_ENCHANTMENTS_ID), Times.Once);

            diseaseStatesEnchantments
                .Verify(x => x.EnchantmentIds, Times.Once);            

            enchantmentGenerator
                .Verify(x => x.Generate(randomizer.Object, ENCHANTMENT_ID), Times.Once);

            diseaseStateFactory
                .Verify(x => x.Create(
                    DISEASE_STATE_ID,
                    DISEASE_STATE_NAME,
                    DISEASE_STATE_PREVIOUS_ID,
                    DISEASE_STATE_NEXT_ID,
                    DISEASE_STATE_SPREAD_ID,
                    It.IsAny<IEnumerable<IEnchantment>>()),
                    Times.Once);

            diseaseStateDefinition
                .Verify(x => x.Id, Times.Once);
            diseaseStateDefinition
                .Verify(x => x.DiseaseStatesEnchantmentsId, Times.Once);
            diseaseStateDefinition
                .Verify(x => x.DiseaseSpreadTypeId, Times.Once);
            diseaseStateDefinition
                .Verify(x => x.Id, Times.Once);
            diseaseStateDefinition
                .Verify(x => x.Name, Times.Once);
            diseaseStateDefinition
                .Verify(x => x.NextStateId, Times.Once);
            diseaseStateDefinition
                .Verify(x => x.PreviousStateId, Times.Once);
        }
    }
}