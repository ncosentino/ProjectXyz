using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Diseases;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Diseases;
using ProjectXyz.Data.Interface.Diseases;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Unit.Diseases
{
    [ApplicationLayer]
    [Diseases]
    public class DiseaseGeneratorTests
    {
        [Fact]
        public void DiseaseGenerator_GenerateForId_Success()
        {
            const string DISEASE_NAME = "The Name";
            Guid DISEASE_ID = Guid.NewGuid();
            Guid DISEASE_STATE_ID1 = Guid.NewGuid();
            Guid DISEASE_STATE_ID2 = Guid.NewGuid();

            var diseaseState1 = new Mock<IDiseaseState>();
            diseaseState1
                .Setup(x => x.NextStateId)
                .Returns(DISEASE_STATE_ID2);

            var diseaseState2 = new Mock<IDiseaseState>();
            diseaseState2
                .Setup(x => x.NextStateId)
                .Returns(Guid.Empty);

            var expectedResult = new Mock<IDisease>();

            var diseaseFactory = new Mock<IDiseaseFactory>();
            diseaseFactory
                .Setup(x => x.Create(DISEASE_ID, DISEASE_NAME, It.IsAny<IEnumerable<IDiseaseState>>()))
                .Callback<Guid, string, IEnumerable<IDiseaseState>>((x, y, z) => z.ToList())
                .Returns(expectedResult.Object);

            var randomizer = new Mock<IRandom>();

            var diseaseStateGenerator = new Mock<IDiseaseStateGenerator>();
            diseaseStateGenerator
                .Setup(x => x.GenerateForId(randomizer.Object, DISEASE_STATE_ID1))
                .Returns(diseaseState1.Object);
            diseaseStateGenerator
                .Setup(x => x.GenerateForId(randomizer.Object, DISEASE_STATE_ID2))
                .Returns(diseaseState2.Object);

            var diseaseDefinition = new Mock<IDiseaseDefinition>();
            diseaseDefinition
                .Setup(x => x.DiseaseStatesId)
                .Returns(DISEASE_STATE_ID1);
            diseaseDefinition
                .Setup(x => x.Name)
                .Returns(DISEASE_NAME);

            var diseaseDefinitionRepository = new Mock<IDiseaseDefinitionRepository>();
            diseaseDefinitionRepository
                .Setup(x => x.GetById(DISEASE_ID))
                .Returns(diseaseDefinition.Object);

            var diseaseGenerator = DiseaseGenerator.Create(
                diseaseFactory.Object,
                diseaseStateGenerator.Object,
                diseaseDefinitionRepository.Object);

            var result = diseaseGenerator.GenerateForId(
                randomizer.Object, 
                DISEASE_ID);
            
            Assert.Equal(expectedResult.Object, result);

            diseaseDefinitionRepository
                .Verify(x => x.GetById(DISEASE_ID), Times.Once);

            diseaseDefinition
                .Verify(x => x.DiseaseStatesId, Times.Once);

            diseaseStateGenerator
                .Verify(x => x.GenerateForId(randomizer.Object, DISEASE_STATE_ID1), Times.Once);
            diseaseStateGenerator
                .Verify(x => x.GenerateForId(randomizer.Object, DISEASE_STATE_ID2), Times.Once);

            diseaseFactory
                .Verify(x => x.Create(DISEASE_ID, DISEASE_NAME, It.IsAny<IEnumerable<IDiseaseState>>()), Times.Once);

            diseaseState1
                .Verify(x => x.NextStateId, Times.Once);

            diseaseState2
                .Verify(x => x.NextStateId, Times.Once);
        }
    }
}