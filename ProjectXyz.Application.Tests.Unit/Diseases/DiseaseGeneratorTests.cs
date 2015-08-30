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
            // Setup
            Guid diseaseNameResourceId = Guid.NewGuid();
            Guid diseaseId = Guid.NewGuid();
            Guid diseaseStateId1 = Guid.NewGuid();
            Guid diseaseStateId2 = Guid.NewGuid();

            var diseaseState1 = new Mock<IDiseaseState>();
            diseaseState1
                .Setup(x => x.NextStateId)
                .Returns(diseaseStateId2);

            var diseaseState2 = new Mock<IDiseaseState>();
            diseaseState2
                .Setup(x => x.NextStateId)
                .Returns(Guid.Empty);

            var expectedResult = new Mock<IDisease>();

            var diseaseFactory = new Mock<IDiseaseFactory>();
            diseaseFactory
                .Setup(x => x.Create(diseaseId, diseaseNameResourceId, It.IsAny<IEnumerable<IDiseaseState>>()))
                .Callback<Guid, Guid, IEnumerable<IDiseaseState>>((x, y, z) => z.ToList())
                .Returns(expectedResult.Object);

            var randomizer = new Mock<IRandom>();

            var diseaseStateGenerator = new Mock<IDiseaseStateGenerator>();
            diseaseStateGenerator
                .Setup(x => x.GenerateForId(randomizer.Object, diseaseStateId1))
                .Returns(diseaseState1.Object);
            diseaseStateGenerator
                .Setup(x => x.GenerateForId(randomizer.Object, diseaseStateId2))
                .Returns(diseaseState2.Object);

            var diseaseDefinition = new Mock<IDiseaseDefinition>();
            diseaseDefinition
                .Setup(x => x.DiseaseStatesId)
                .Returns(diseaseStateId1);
            diseaseDefinition
                .Setup(x => x.NameStringResourceId)
                .Returns(diseaseNameResourceId);

            var diseaseDefinitionRepository = new Mock<IDiseaseDefinitionRepository>();
            diseaseDefinitionRepository
                .Setup(x => x.GetById(diseaseId))
                .Returns(diseaseDefinition.Object);

            var diseaseGenerator = DiseaseGenerator.Create(
                diseaseFactory.Object,
                diseaseStateGenerator.Object,
                diseaseDefinitionRepository.Object);

            // Execute
            var result = diseaseGenerator.GenerateForId(
                randomizer.Object, 
                diseaseId);
            
            // Assert
            Assert.Equal(expectedResult.Object, result);

            diseaseDefinitionRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);

            diseaseDefinition.Verify(x => x.DiseaseStatesId, Times.Once);

            diseaseStateGenerator.Verify(x => x.GenerateForId(It.IsAny<IRandom>(), It.IsAny<Guid>()), Times.Exactly(2));

            diseaseFactory.Verify(x => x.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<IEnumerable<IDiseaseState>>()), Times.Once);

            diseaseState1.Verify(x => x.NextStateId, Times.Once);

            diseaseState2.Verify(x => x.NextStateId, Times.Once);
        }
    }
}