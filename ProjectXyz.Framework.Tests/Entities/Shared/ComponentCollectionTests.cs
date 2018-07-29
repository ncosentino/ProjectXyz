using System.Linq;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Shared.Framework.Entities;
using Xunit;

namespace ProjectXyz.Framework.Tests.Entities.Shared
{
    public sealed class ComponentCollectionTests
    {
        [Fact]
        private void Get_SingleMatchingType_SingleResult()
        {
            // Setup
            var components = new IComponent[]
            {
                new GenericComponent<ITypeA>(new TypeA()),
                new GenericComponent<int>(1),
            };
            var componentCollection = new ComponentCollection(components);

            // Execute
            var results = componentCollection
                .Get<IComponent<ITypeA>>()
                .ToArray();

            // Assert
            Assert.Single(results);
            Assert.Equal(components[0], results.Single());
        }

        [Fact]
        private void Get_MultipleMatchingType_MultipleResults()
        {
            // Setup
            var components = new IComponent[]
            {
                new GenericComponent<ITypeA>(new TypeA()),
                new GenericComponent<ITypeA>(new TypeA()),
                new GenericComponent<int>(1),
            };
            var componentCollection = new ComponentCollection(components);

            // Execute
            var results = componentCollection
                .Get<IComponent<ITypeA>>()
                .ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(components[0], results[0]);
            Assert.Equal(components[1], results[1]);
        }

        [Fact]
        private void Get_ChildWrappedAsChildQueriedAsParent_SingleResult()
        {
            // Setup
            var components = new IComponent[]
            {
                new GenericComponent<ITypeB>(new TypeB()),
                new GenericComponent<int>(1),
            };
            var componentCollection = new ComponentCollection(components);

            // Execute
            var results = componentCollection
                .Get<IComponent<ITypeA>>()
                .ToArray();

            // Assert
            Assert.Single(results);
            Assert.Equal(components[0], results.Single());
        }

        [Fact]
        private void Get_ChildWrappedAsParentQueriedAsChild_NoResult()
        {
            // Setup
            var components = new IComponent[]
            {
                new GenericComponent<ITypeA>(new TypeB()),
                new GenericComponent<int>(1),
            };
            var componentCollection = new ComponentCollection(components);

            // Execute
            var results = componentCollection
                .Get<IComponent<ITypeB>>()
                .ToArray();

            // Assert
            Assert.Empty(results);
        }

        private interface ITypeA
        {   
        }

        private interface ITypeB : ITypeA
        {
        }

        private sealed class TypeA : ITypeA
        {
        }

        private sealed class TypeB : ITypeB
        {
        }
    }
}
