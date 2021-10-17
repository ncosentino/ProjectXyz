using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using NexusLabs.Framework;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
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
        private readonly Mock<IFilterContext> _filterContext;
        private readonly IFilterAttribute[] _filterContextAttributes;
        private readonly Mock<IEnumerable<IDropTable>> _unfilteredDropTables;

        public LootGeneratorTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            
            _dropTableRepository = _mockRepository.Create<IDropTableRepositoryFacade>();
            _dropTableHandlerGeneratorFacade = _mockRepository.Create<IDropTableHandlerGeneratorFacade>();
            _attributeFilterer = _mockRepository.Create<IAttributeFilterer>();
            _random = _mockRepository.Create<IRandom>();
            _filterContext = _mockRepository.Create<IFilterContext>();
            _filterContextAttributes = new IFilterAttribute[] { };
            _unfilteredDropTables = _mockRepository.Create<IEnumerable<IDropTable>>();

            _lootGenerator = new LootGenerator(
                _dropTableRepository.Object,
                _dropTableHandlerGeneratorFacade.Object,
                _attributeFilterer.Object,
                _random.Object);
        }

        [Fact]
        public void GenerateLoot_Min0Max0Match1_NoItems()
        {
            // arrange
            var filteredDropTables = new IDropTable[]
            {
                _mockRepository.Create<IDropTable>().Object,
            };

            _dropTableRepository
                .Setup(x => x.GetAllDropTables())
                .Returns(_unfilteredDropTables.Object);

            _filterContext
                .Setup(x => x.Attributes)
                .Returns(_filterContextAttributes);

            _attributeFilterer
                .Setup(x => x.BidirectionalFilter(
                    _unfilteredDropTables.Object,
                    _filterContextAttributes))
                .Returns(filteredDropTables);

            _filterContext
                .Setup(x => x.MinimumCount)
                .Returns(0);
            _filterContext
                .Setup(x => x.MaximumCount)
                .Returns(0);

            _random
                .Setup(x => x.Next(0, 1)) // 1 is the exclusive upper bound
                .Returns(0);

            // act
            var result = _lootGenerator
                .GenerateLoot(_filterContext.Object)
                .ToArray();

            // assert
            Assert.Empty(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_Min0Match0_NoItems()
        {
            // arrange
            var filteredDropTables = new IDropTable[]
            {
            };

            _dropTableRepository
                .Setup(x => x.GetAllDropTables())
                .Returns(_unfilteredDropTables.Object);

            _filterContext
                .Setup(x => x.Attributes)
                .Returns(_filterContextAttributes);

            _attributeFilterer
                .Setup(x => x.BidirectionalFilter(
                    _unfilteredDropTables.Object,
                    _filterContextAttributes))
                .Returns(filteredDropTables);

            _filterContext
                .Setup(x => x.MinimumCount)
                .Returns(0);

            // act
            var result = _lootGenerator
                .GenerateLoot(_filterContext.Object)
                .ToArray();

            // assert
            Assert.Empty(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_Min1Match0_Throws()
        {
            // arrange
            var filteredDropTables = new IDropTable[0];

            _dropTableRepository
                .Setup(x => x.GetAllDropTables())
                .Returns(_unfilteredDropTables.Object);

            _filterContext
                .Setup(x => x.Attributes)
                .Returns(_filterContextAttributes);

            _attributeFilterer
                .Setup(x => x.BidirectionalFilter(
                    _unfilteredDropTables.Object,
                    _filterContextAttributes))
                .Returns(filteredDropTables);

            _filterContext
                .Setup(x => x.MinimumCount)
                .Returns(1);
            _filterContext
                .Setup(x => x.MaximumCount)
                .Returns(1);

            _random
                .Setup(x => x.Next(1, 2)) // 2 is the exclusive upper bound
                .Returns(1);

            // act
            var exception = Assert.Throws<InvalidOperationException>(() => _lootGenerator
                .GenerateLoot(_filterContext.Object)
                .ToArray());

            // assert
            Assert.Equal(
                $"There was no drop table that could be selected from " +
                $"the set of filtered drop tables using context '{_filterContext.Object}'.",
                exception.Message);
        }

        [Fact]
        public void GenerateLoot_Min0Max1Match1Generate0_NoItems()
        {
            // arrange
            var dropTable = _mockRepository.Create<IDropTable>();

            _dropTableHandlerGeneratorFacade
                .Setup(x => x.GenerateLoot(
                    dropTable.Object,
                    _filterContext.Object))
                .Returns(Enumerable.Empty<IGameObject>());

            var filteredDropTables = new IDropTable[]
            {
                dropTable.Object,
            };

            _dropTableRepository
                .Setup(x => x.GetAllDropTables())
                .Returns(_unfilteredDropTables.Object);

            _filterContext
                .Setup(x => x.Attributes)
                .Returns(_filterContextAttributes);

            _attributeFilterer
                .Setup(x => x.BidirectionalFilter(
                    _unfilteredDropTables.Object,
                    _filterContextAttributes))
                .Returns(filteredDropTables);

            _filterContext
                .Setup(x => x.MinimumCount)
                .Returns(0);
            _filterContext
                .Setup(x => x.MaximumCount)
                .Returns(1);

            _random
                .Setup(x => x.Next(0, 1)) // 1 is the exclusive upper bound for tables to roll
                .Returns(0);
            _random
                .Setup(x => x.Next(0, 2)) // 2 is the exclusive upper bound for target count
                .Returns(1);

            // act
            var result = _lootGenerator
                .GenerateLoot(_filterContext.Object)
                .ToArray();

            // assert
            Assert.Empty(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_Max3TableGenerates5_3Items()
        {
            // arrange
            var generatedItems = new[]
            {
                _mockRepository.Create<IGameObject>().Object,
                _mockRepository.Create<IGameObject>().Object,
                _mockRepository.Create<IGameObject>().Object,
                _mockRepository.Create<IGameObject>().Object,
                _mockRepository.Create<IGameObject>().Object,
            };

            var dropTable = _mockRepository.Create<IDropTable>();

            _dropTableHandlerGeneratorFacade
                .Setup(x => x.GenerateLoot(
                    dropTable.Object,
                    _filterContext.Object))
                .Returns(generatedItems);

            var filteredDropTables = new IDropTable[]
            {
                dropTable.Object,
            };

            _dropTableRepository
                .Setup(x => x.GetAllDropTables())
                .Returns(_unfilteredDropTables.Object);

            _filterContext
                .Setup(x => x.Attributes)
                .Returns(_filterContextAttributes);

            _attributeFilterer
                .Setup(x => x.BidirectionalFilter(
                    _unfilteredDropTables.Object,
                    _filterContextAttributes))
                .Returns(filteredDropTables);

            _filterContext
                .Setup(x => x.MinimumCount)
                .Returns(0);
            _filterContext
                .Setup(x => x.MaximumCount)
                .Returns(3);

            _random
                .Setup(x => x.Next(0, 1)) // 1 is the exclusive upper bound for tables to roll
                .Returns(0);
            _random
                .Setup(x => x.Next(0, 4)) // 2 is the exclusive upper bound for target count
                .Returns(3);

            // act
            var result = _lootGenerator
                .GenerateLoot(_filterContext.Object)
                .ToArray();

            // assert
            Assert.Equal<IGameObject>(
                generatedItems.Take(3),
                result);
            _mockRepository.VerifyAll();
        }
    }
}
