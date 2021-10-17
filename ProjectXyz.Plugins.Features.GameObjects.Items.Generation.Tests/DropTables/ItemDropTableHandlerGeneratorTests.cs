using System.Collections.Generic;
using System.Linq;

using Moq;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.Tests.DropTables
{
    public sealed class ItemDropTableHandlerGeneratorTests
    {
        private readonly MockRepository _mockRepository;
        private readonly ItemDropTableHandlerGenerator _itemDropTableHandlerGenerator;
        private readonly Mock<IItemGeneratorFacade> _itemGeneratorFacade;
        private readonly Mock<IFilterContextFactory> _filterContextFactory;
        private readonly Mock<IFilterContext> _sourceFilterContext;
        private readonly Mock<IFilterContext> _newFilterContext;

        public ItemDropTableHandlerGeneratorTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _itemGeneratorFacade = _mockRepository.Create<IItemGeneratorFacade>();
            _filterContextFactory = _mockRepository.Create<IFilterContextFactory>();
            _sourceFilterContext = _mockRepository.Create<IFilterContext>();
            _newFilterContext = _mockRepository.Create<IFilterContext>();

            _itemDropTableHandlerGenerator = new ItemDropTableHandlerGenerator(
                _itemGeneratorFacade.Object,
                _filterContextFactory.Object);
        }

        [Fact]
        public void DropTableType_Expected()
        {
            Assert.Equal(
                typeof(ItemDropTable),
                _itemDropTableHandlerGenerator.DropTableType);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_MistmatchDropTableType_Throws()
        {
            var dropTable = _mockRepository.Create<IDropTable>();
            
            var exception = Assert.Throws<ContractException>(() => _itemDropTableHandlerGenerator.GenerateLoot(
                dropTable.Object,
                _sourceFilterContext.Object));

            Assert.Equal(
                $"The provided drop table '{dropTable.Object}' must have " +
                $"the type '{typeof(ItemDropTable)}'.",
                exception.Message);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_NoContextAttributesNoTableAttributes_ExpectedContext()
        {
            var dropTable = new ItemDropTable(
                new StringIdentifier("TheDropTable"),
                123,
                456,
                new IFilterAttribute[] { },
                new IFilterAttribute[] { });
            var expectedItems = new IGameObject[]
            {
                _mockRepository.Create<IGameObject>().Object,
            };

            _itemGeneratorFacade
                .Setup(x => x.GenerateItems(_newFilterContext.Object))
                .Returns(expectedItems);

            _sourceFilterContext
                .Setup(x => x.Attributes)
                .Returns(new IFilterAttribute[]
                {
                });

            _filterContextFactory
                .Setup(x => x.CreateContext(
                    123,
                    456,
                    It.Is<IEnumerable<IFilterAttribute>>(attrs =>
                        attrs.SequenceEqual(new IFilterAttribute[]
                        {
                        }))))
                .Returns(_newFilterContext.Object);

            var results = _itemDropTableHandlerGenerator
                .GenerateLoot(
                    dropTable,
                    _sourceFilterContext.Object)
                .ToArray();

            Assert.Equal<IGameObject>(expectedItems, results);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_NoContextAttributesTableProvidedAttributes_ExpectedContext()
        {
            var dropTableProvidedAttribute = _mockRepository.Create<IFilterAttribute>();

            var dropTable = new ItemDropTable(
                new StringIdentifier("TheDropTable"),
                123,
                456,
                Enumerable.Empty<IFilterAttribute>(),
                new[]
                {
                    dropTableProvidedAttribute.Object,
                });
            var expectedItems = new IGameObject[]
            {
                _mockRepository.Create<IGameObject>().Object,
            };

            _itemGeneratorFacade
                .Setup(x => x.GenerateItems(_newFilterContext.Object))
                .Returns(expectedItems);

            _sourceFilterContext
                .Setup(x => x.Attributes)
                .Returns(new IFilterAttribute[]
                {
                });

            _filterContextFactory
                .Setup(x => x.CreateContext(
                    123,
                    456,
                    It.Is<IEnumerable<IFilterAttribute>>(attrs =>
                        attrs.SequenceEqual(new IFilterAttribute[]
                        {
                            dropTableProvidedAttribute.Object,
                        }))))
                .Returns(_newFilterContext.Object);

            var results = _itemDropTableHandlerGenerator
                .GenerateLoot(
                    dropTable,
                    _sourceFilterContext.Object)
                .ToArray();

            Assert.Equal<IGameObject>(expectedItems, results);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_RequiredContextAttributesTableProvidedAttributes_ExpectedContext()
        {
            var dropTableProvidedAttribute = _mockRepository.Create<IFilterAttribute>();

            var dropTable = new ItemDropTable(
                new StringIdentifier("TheDropTable"),
                123,
                456,
                Enumerable.Empty<IFilterAttribute>(),
                new[]
                {
                    dropTableProvidedAttribute.Object,
                });
            var expectedItems = new IGameObject[]
            {
                _mockRepository.Create<IGameObject>().Object,
            };

            _itemGeneratorFacade
                .Setup(x => x.GenerateItems(_newFilterContext.Object))
                .Returns(expectedItems);

            var sourceFilterAttribute = _mockRepository.Create<IFilterAttribute>();
            var sourceFilterAttributeValue = _mockRepository.Create<IFilterAttributeValue>();
            sourceFilterAttribute
                .Setup(x => x.Id)
                .Returns(new StringIdentifier("context required attribute"));
            sourceFilterAttribute
                .Setup(x => x.Required)
                .Returns(true);
            sourceFilterAttribute
                .Setup(x => x.Value)
                .Returns(sourceFilterAttributeValue.Object);

            _sourceFilterContext
                .Setup(x => x.Attributes)
                .Returns(new IFilterAttribute[]
                {
                    sourceFilterAttribute.Object,
                });

            _filterContextFactory
                .Setup(x => x.CreateContext(
                    123,
                    456,
                    It.Is<IEnumerable<IFilterAttribute>>(attrs =>
                        attrs.Any(attr => attr.Equals(dropTableProvidedAttribute.Object)) &&
                        attrs.Any(attr => 
                            attr.Id.Equals(new StringIdentifier("context required attribute")) &&
                            attr.Required == false &&
                            attr.Value == sourceFilterAttributeValue.Object) &&
                        attrs.Count() == 2)))
                .Returns(_newFilterContext.Object);

            var results = _itemDropTableHandlerGenerator
                .GenerateLoot(
                    dropTable,
                    _sourceFilterContext.Object)
                .ToArray();

            Assert.Equal<IGameObject>(expectedItems, results);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_NotRequiredContextAttributesTableProvidedAttributes_ExpectedContext()
        {
            var dropTableProvidedAttribute = _mockRepository.Create<IFilterAttribute>();

            var dropTable = new ItemDropTable(
                new StringIdentifier("TheDropTable"),
                123,
                456,
                Enumerable.Empty<IFilterAttribute>(),
                new[]
                {
                    dropTableProvidedAttribute.Object,
                });
            var expectedItems = new IGameObject[]
            {
                _mockRepository.Create<IGameObject>().Object,
            };

            _itemGeneratorFacade
                .Setup(x => x.GenerateItems(_newFilterContext.Object))
                .Returns(expectedItems);

            var sourceFilterAttribute = _mockRepository.Create<IFilterAttribute>();
            sourceFilterAttribute
                .Setup(x => x.Required)
                .Returns(false);

            _sourceFilterContext
                .Setup(x => x.Attributes)
                .Returns(new IFilterAttribute[]
                {
                    sourceFilterAttribute.Object,
                });

            _filterContextFactory
                .Setup(x => x.CreateContext(
                    123,
                    456,
                    It.Is<IEnumerable<IFilterAttribute>>(attrs =>
                        attrs.Any(attr => attr.Equals(dropTableProvidedAttribute.Object)) &&
                        attrs.Any(attr => attr.Equals(sourceFilterAttribute.Object)) &&
                        attrs.Count() == 2)))
                .Returns(_newFilterContext.Object);

            var results = _itemDropTableHandlerGenerator
                .GenerateLoot(
                    dropTable,
                    _sourceFilterContext.Object)
                .ToArray();

            Assert.Equal<IGameObject>(expectedItems, results);
            _mockRepository.VerifyAll();
        }
    }
}
