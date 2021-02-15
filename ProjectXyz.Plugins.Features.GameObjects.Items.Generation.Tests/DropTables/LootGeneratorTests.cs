using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;

using NexusLabs.Framework;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using Xunit;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.Tests.DropTables
{
    public sealed class LootGeneratorTests
    {
        private readonly MockRepository _mockRepository;
        private readonly LootGenerator _lootGenerator;
        private readonly Mock<IDropTableRepositoryFacade> _dropTableRepository;
        private readonly Mock<IDropTableHandlerGeneratorFacade> _dropTableHandlerGeneratorFacade;
        private readonly Mock<IAttributeFilterer> _attributeFilterer;
        private readonly Mock<IRandom> _random;
        private readonly Mock<IGeneratorContext> _generatorContext;
        private readonly Mock<IEnumerable<IDropTable>> _unfilteredDropTables;

        public LootGeneratorTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            
            _dropTableRepository = _mockRepository.Create<IDropTableRepositoryFacade>();
            _dropTableHandlerGeneratorFacade = _mockRepository.Create<IDropTableHandlerGeneratorFacade>();
            _attributeFilterer = _mockRepository.Create<IAttributeFilterer>();
            _random = _mockRepository.Create<IRandom>();
            _generatorContext = _mockRepository.Create<IGeneratorContext>();
            _unfilteredDropTables = _mockRepository.Create<IEnumerable<IDropTable>>();

            _lootGenerator = new LootGenerator(
                _dropTableRepository.Object,
                _dropTableHandlerGeneratorFacade.Object,
                _attributeFilterer.Object,
                _random.Object);
        }

        [Fact]
        public void GenerateLoot_MinimumGenerateCountZero_NoItems()
        {
            // arrange
            var filteredDropTables = new IDropTable[0];

            _dropTableRepository
                .Setup(x => x.GetAllDropTables())
                .Returns(_unfilteredDropTables.Object);

            _attributeFilterer
                .Setup(x => x.Filter(
                    _unfilteredDropTables.Object,
                    _generatorContext.Object))
                .Returns(filteredDropTables);

            _generatorContext
                .Setup(x => x.MinimumGenerateCount)
                .Returns(0);

            // act
            var result = _lootGenerator
                .GenerateLoot(_generatorContext.Object)
                .ToArray();

            // assert
            Assert.Empty(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_MaximumGenerateCountZero_NoItems()
        {
            // arrange
            var filteredDropTables = new IDropTable[0];

            _dropTableRepository
                .Setup(x => x.GetAllDropTables())
                .Returns(_unfilteredDropTables.Object);

            _attributeFilterer
                .Setup(x => x.Filter(
                    _unfilteredDropTables.Object,
                    _generatorContext.Object))
                .Returns(filteredDropTables);

            _generatorContext
                .Setup(x => x.MinimumGenerateCount)
                .Returns(1);
            _generatorContext
                .Setup(x => x.MaximumGenerateCount)
                .Returns(0);

            // act
            var result = _lootGenerator
                .GenerateLoot(_generatorContext.Object)
                .ToArray();

            // assert
            Assert.Empty(result);
            _mockRepository.VerifyAll();
        }
    }
}
