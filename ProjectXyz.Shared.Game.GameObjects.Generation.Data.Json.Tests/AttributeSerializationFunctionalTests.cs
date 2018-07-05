using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
using Xunit;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Tests
{
    public sealed class AttributeSerializationFunctionalTests
    {
        private static readonly ISerializer _serializer;
        private static readonly IDeserializer _deserializer;

        static AttributeSerializationFunctionalTests()
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            var moduleDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var modules = moduleDiscoverer
                .Discover(moduleDirectory, "*.exe")
                .Concat(moduleDiscoverer
                    .Discover(moduleDirectory, "*.dll"));
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);

            _serializer = dependencyContainer.Resolve<ISerializer>();
            _deserializer = dependencyContainer.Resolve<IDeserializer>();
        }

        [ClassData(typeof(TestData))]
        [Theory]
        private void Deserialize_SerializedData_ExpectedResult(
            object dataToSerialize,
            Action<object> validateCallback)
        {
            var serializedStream = new MemoryStream();
            _serializer.Serialize(
                serializedStream,
                dataToSerialize);
            serializedStream.Seek(0, SeekOrigin.Begin);
            var deserialized = _deserializer.Deserialize<IGeneratorAttributeValue>(serializedStream);
            validateCallback(deserialized);
        }

        private sealed class TestData : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    new StringGeneratorAttributeValue("my string value"),
                    new Action<object>(result =>
                    {
                        Assert.IsType<StringGeneratorAttributeValue>(result);
                        Assert.Equal("my string value", ((StringGeneratorAttributeValue)result).Value);
                    }),
                },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
