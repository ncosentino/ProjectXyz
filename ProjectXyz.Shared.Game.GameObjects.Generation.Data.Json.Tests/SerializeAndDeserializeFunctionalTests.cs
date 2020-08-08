using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Autofac;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Tests
{
    public sealed class SerializeAndDeserializeFunctionalTests
    {
        private static readonly ISerializer _serializer;
        private static readonly IDeserializer _deserializer;

        static SerializeAndDeserializeFunctionalTests()
        {
            var scope = new TestLifeTimeScopeFactory().CreateScope();

            _serializer = scope.Resolve<ISerializer>();
            _deserializer = scope.Resolve<IDeserializer>();
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
            var deserialized = _deserializer.Deserialize(serializedStream);
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
                new object[]
                {
                    new GeneratorComponent(new[]
                    {
                        new GeneratorAttribute(
                            new StringIdentifier("attr1"),
                            new StringGeneratorAttributeValue("string"),
                            true),
                        new GeneratorAttribute(
                            new StringIdentifier("attr2"),
                            new DoubleGeneratorAttributeValue(123),
                            false),
                    }),
                    new Action<object>(result =>
                    {
                        Assert.IsType<GeneratorComponent>(result);

                        var generatorComponent = (GeneratorComponent)result;
                        Assert.Equal(2, generatorComponent.SupportedAttributes.Count());

                        var firstAttribute = generatorComponent.SupportedAttributes.First();
                        Assert.IsType<GeneratorAttribute>(firstAttribute);
                        Assert.Equal(
                            new StringIdentifier("attr1"),
                            firstAttribute.Id);
                        Assert.IsType<StringGeneratorAttributeValue>(firstAttribute.Value);
                        Assert.Equal("string", ((StringGeneratorAttributeValue)firstAttribute.Value).Value);
                        Assert.True(firstAttribute.Required, $"Unexpected value for '{nameof(IGeneratorAttribute.Required)}'.");

                        var secondAttribute = generatorComponent.SupportedAttributes.Last();
                        Assert.IsType<GeneratorAttribute>(secondAttribute);
                        Assert.Equal(
                            new StringIdentifier("attr2"),
                            secondAttribute.Id);
                        Assert.IsType<DoubleGeneratorAttributeValue>(secondAttribute.Value);
                        Assert.Equal(123d, ((DoubleGeneratorAttributeValue)secondAttribute.Value).Value);
                        Assert.False(secondAttribute.Required, $"Unexpected value for '{nameof(IGeneratorAttribute.Required)}'.");
                    }),
                },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
