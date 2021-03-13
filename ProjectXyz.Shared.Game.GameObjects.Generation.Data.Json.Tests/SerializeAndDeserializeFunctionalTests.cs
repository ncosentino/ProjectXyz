using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Autofac;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Shared.Framework;
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
                dataToSerialize,
                Encoding.UTF8);
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
                    new StringFilterAttributeValue("my string value"),
                    new Action<object>(result =>
                    {
                        Assert.IsType<StringFilterAttributeValue>(result);
                        Assert.Equal("my string value", ((StringFilterAttributeValue)result).Value);
                    }),
                },
                new object[]
                {
                    new HasFilterAttributes(new[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("attr1"),
                            new StringFilterAttributeValue("string"),
                            true),
                        new FilterAttribute(
                            new StringIdentifier("attr2"),
                            new DoubleFilterAttributeValue(123),
                            false),
                    }),
                    new Action<object>(result =>
                    {
                        Assert.IsType<HasFilterAttributes>(result);

                        var filterComponent = (HasFilterAttributes)result;
                        Assert.Equal(2, filterComponent.SupportedAttributes.Count());

                        var firstAttribute = filterComponent.SupportedAttributes.First();
                        Assert.IsType<FilterAttribute>(firstAttribute);
                        Assert.Equal(
                            new StringIdentifier("attr1"),
                            firstAttribute.Id);
                        Assert.IsType<StringFilterAttributeValue>(firstAttribute.Value);
                        Assert.Equal("string", ((StringFilterAttributeValue)firstAttribute.Value).Value);
                        Assert.True(firstAttribute.Required, $"Unexpected value for '{nameof(IFilterAttribute.Required)}'.");

                        var secondAttribute = filterComponent.SupportedAttributes.Last();
                        Assert.IsType<FilterAttribute>(secondAttribute);
                        Assert.Equal(
                            new StringIdentifier("attr2"),
                            secondAttribute.Id);
                        Assert.IsType<DoubleFilterAttributeValue>(secondAttribute.Value);
                        Assert.Equal(123d, ((DoubleFilterAttributeValue)secondAttribute.Value).Value);
                        Assert.False(secondAttribute.Required, $"Unexpected value for '{nameof(IFilterAttribute.Required)}'.");
                    }),
                }
            };

            private sealed class RepeatedPropertyRefObject
            {
                public SingletonObject Property1 { get; set; }

                public SingletonObject Property2 { get; set; }
            }

            private sealed class SingletonObject
            {
                private static Lazy<SingletonObject> _lazySingle = new Lazy<SingletonObject>(() => new SingletonObject());

                public static SingletonObject Value => _lazySingle.Value;
            }

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }

    public sealed class HasFilterAttributes : IHasFilterAttributes
    {
        public HasFilterAttributes()
            : this(Enumerable.Empty<IFilterAttribute>())
        {
        }

        public HasFilterAttributes(IEnumerable<IFilterAttribute> supportedAttributes)
        {
            SupportedAttributes = supportedAttributes;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}
