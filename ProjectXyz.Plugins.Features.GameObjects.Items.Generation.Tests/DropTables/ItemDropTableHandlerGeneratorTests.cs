using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;

using NexusLabs.Contracts;
using NexusLabs.Framework;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
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
        private readonly Mock<IGeneratorContextFactory> _generatorContextFactory;
        private readonly Mock<IGeneratorContext> _sourceGeneratorContext;
        private readonly Mock<IGeneratorContext> _newGeneratorContext;

        public ItemDropTableHandlerGeneratorTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _itemGeneratorFacade = _mockRepository.Create<IItemGeneratorFacade>();
            _generatorContextFactory = _mockRepository.Create<IGeneratorContextFactory>();
            _sourceGeneratorContext = _mockRepository.Create<IGeneratorContext>();
            _newGeneratorContext = _mockRepository.Create<IGeneratorContext>();

            _itemDropTableHandlerGenerator = new ItemDropTableHandlerGenerator(
                _itemGeneratorFacade.Object,
                _generatorContextFactory.Object);
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
                _sourceGeneratorContext.Object));

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
                456);
            var expectedItems = new IGameObject[]
            {
                _mockRepository.Create<IGameObject>().Object,
            };

            _itemGeneratorFacade
                .Setup(x => x.GenerateItems(_newGeneratorContext.Object))
                .Returns(expectedItems);

            _sourceGeneratorContext
                .Setup(x => x.Attributes)
                .Returns(new IGeneratorAttribute[]
                {
                });

            _generatorContextFactory
                .Setup(x => x.CreateGeneratorContext(
                    123,
                    456,
                    It.Is<IEnumerable<IGeneratorAttribute>>(attrs =>
                        attrs.SequenceEqual(new IGeneratorAttribute[]
                        {
                        }))))
                .Returns(_newGeneratorContext.Object);

            var results = _itemDropTableHandlerGenerator
                .GenerateLoot(
                    dropTable,
                    _sourceGeneratorContext.Object)
                .ToArray();

            Assert.Equal<IGameObject>(expectedItems, results);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_NoContextAttributesTableProvidedAttributes_ExpectedContext()
        {
            var dropTableProvidedAttribute = _mockRepository.Create<IGeneratorAttribute>();

            var dropTable = new ItemDropTable(
                new StringIdentifier("TheDropTable"),
                123,
                456,
                Enumerable.Empty<IGeneratorAttribute>(),
                new[]
                {
                    dropTableProvidedAttribute.Object,
                });
            var expectedItems = new IGameObject[]
            {
                _mockRepository.Create<IGameObject>().Object,
            };

            _itemGeneratorFacade
                .Setup(x => x.GenerateItems(_newGeneratorContext.Object))
                .Returns(expectedItems);

            _sourceGeneratorContext
                .Setup(x => x.Attributes)
                .Returns(new IGeneratorAttribute[]
                {
                });

            _generatorContextFactory
                .Setup(x => x.CreateGeneratorContext(
                    123,
                    456,
                    It.Is<IEnumerable<IGeneratorAttribute>>(attrs =>
                        attrs.SequenceEqual(new IGeneratorAttribute[]
                        {
                            dropTableProvidedAttribute.Object,
                        }))))
                .Returns(_newGeneratorContext.Object);

            var results = _itemDropTableHandlerGenerator
                .GenerateLoot(
                    dropTable,
                    _sourceGeneratorContext.Object)
                .ToArray();

            Assert.Equal<IGameObject>(expectedItems, results);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void GenerateLoot_RequiredContextAttributesTableProvidedAttributes_ExpectedContext()
        {
            var dropTableProvidedAttribute = _mockRepository.Create<IGeneratorAttribute>();

            var dropTable = new ItemDropTable(
                new StringIdentifier("TheDropTable"),
                123,
                456,
                Enumerable.Empty<IGeneratorAttribute>(),
                new[]
                {
                    dropTableProvidedAttribute.Object,
                });
            var expectedItems = new IGameObject[]
            {
                _mockRepository.Create<IGameObject>().Object,
            };

            _itemGeneratorFacade
                .Setup(x => x.GenerateItems(_newGeneratorContext.Object))
                .Returns(expectedItems);

            var sourceGeneratorAttribute = _mockRepository.Create<IGeneratorAttribute>();
            var sourceGeneratorAttributeValue = _mockRepository.Create<IGeneratorAttributeValue>();
            sourceGeneratorAttribute
                .Setup(x => x.Id)
                .Returns(new StringIdentifier("context required attribute"));
            sourceGeneratorAttribute
                .Setup(x => x.Required)
                .Returns(true);
            sourceGeneratorAttribute
                .Setup(x => x.Value)
                .Returns(sourceGeneratorAttributeValue.Object);

            _sourceGeneratorContext
                .Setup(x => x.Attributes)
                .Returns(new IGeneratorAttribute[]
                {
                    sourceGeneratorAttribute.Object,
                });

            _generatorContextFactory
                .Setup(x => x.CreateGeneratorContext(
                    123,
                    456,
                    It.Is<IEnumerable<IGeneratorAttribute>>(attrs =>
                        attrs.Any(attr => attr.Equals(dropTableProvidedAttribute.Object)) &&
                        attrs.Any(attr => 
                            attr.Id.Equals(new StringIdentifier("context required attribute")) &&
                            attr.Required == false &&
                            attr.Value == sourceGeneratorAttributeValue.Object) &&
                        attrs.Count() == 2)))
                .Returns(_newGeneratorContext.Object);

            var results = _itemDropTableHandlerGenerator
                .GenerateLoot(
                    dropTable,
                    _sourceGeneratorContext.Object)
                .ToArray();

            Assert.Equal<IGameObject>(expectedItems, results);
            _mockRepository.VerifyAll();
        }

        public void GenerateLoot_NotRequiredContextAttributesTableProvidedAttributes_ExpectedContext()
        {
            var dropTableProvidedAttribute = _mockRepository.Create<IGeneratorAttribute>();

            var dropTable = new ItemDropTable(
                new StringIdentifier("TheDropTable"),
                123,
                456,
                Enumerable.Empty<IGeneratorAttribute>(),
                new[]
                {
                    dropTableProvidedAttribute.Object,
                });
            var expectedItems = new IGameObject[]
            {
                _mockRepository.Create<IGameObject>().Object,
            };

            _itemGeneratorFacade
                .Setup(x => x.GenerateItems(_newGeneratorContext.Object))
                .Returns(expectedItems);

            var sourceGeneratorAttribute = _mockRepository.Create<IGeneratorAttribute>();
            sourceGeneratorAttribute
                .Setup(x => x.Required)
                .Returns(false);

            _sourceGeneratorContext
                .Setup(x => x.Attributes)
                .Returns(new IGeneratorAttribute[]
                {
                    sourceGeneratorAttribute.Object,
                });

            _generatorContextFactory
                .Setup(x => x.CreateGeneratorContext(
                    123,
                    456,
                    It.Is<IEnumerable<IGeneratorAttribute>>(attrs =>
                        attrs.Any(attr => attr.Equals(dropTableProvidedAttribute.Object)) &&
                        attrs.Any(attr => attr.Equals(sourceGeneratorAttribute.Object)) &&
                        attrs.Count() == 2)))
                .Returns(_newGeneratorContext.Object);

            var results = _itemDropTableHandlerGenerator
                .GenerateLoot(
                    dropTable,
                    _sourceGeneratorContext.Object)
                .ToArray();

            Assert.Equal<IGameObject>(expectedItems, results);
            _mockRepository.VerifyAll();
        }
    }
}
