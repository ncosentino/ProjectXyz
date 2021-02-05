using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Autofac;

using Newtonsoft.Json;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft;
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
            var jsonSerializer = new JsonSerializer();
            _serializer = new NewtonsoftJsonSerializer(jsonSerializer);
            _deserializer = new NewtonsoftJsonDeserializer(jsonSerializer);

            var xx = new TestLifeTimeScopeFactory();
            var scope = xx.CreateScope();
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
                    new RepeatedPropertyRefObject()
                    {
                        Property1 = SingletonObject.Value,
                        Property2 = SingletonObject.Value,
                    },
                    new Action<object>(result =>
                    {
                        Assert.IsType<RepeatedPropertyRefObject>(result);
                        Assert.NotNull(((RepeatedPropertyRefObject)result).Property1);
                        Assert.NotNull(((RepeatedPropertyRefObject)result).Property2);
                    }),
                },
                new object[]
                {
                    new[] { SingletonObject.Value, SingletonObject.Value },
                    new Action<object>(result =>
                    {
                        Assert.IsAssignableFrom<IReadOnlyList<object>>(result);
                        Assert.Equal(2, ((IReadOnlyList<object>)result).Count);
                        Assert.IsType<SingletonObject>(((IReadOnlyList<object>)result)[0]);
                        Assert.IsType<SingletonObject>(((IReadOnlyList<object>)result)[1]);
                    }),
                },
                new object[]
                {
                    new[] { "test", "array" },
                    new Action<object>(result =>
                    {
                        Assert.IsAssignableFrom<IReadOnlyList<object>>(result);
                        Assert.Equal(2, ((IReadOnlyList<object>)result).Count);
                        Assert.Equal("test", ((IReadOnlyList<object>)result)[0]);
                        Assert.Equal("array", ((IReadOnlyList<object>)result)[1]);
                    }),
                },
                new object[]
                {
                    new[] { 12.3d, 45.6d },
                    new Action<object>(result =>
                    {
                        Assert.IsAssignableFrom<IReadOnlyCollection<object>>(result);
                        Assert.Equal(2, ((IReadOnlyList<object>)result).Count);
                        Assert.Equal(12.3d, ((IReadOnlyList<object>)result)[0]);
                        Assert.Equal(45.6d, ((IReadOnlyList<object>)result)[1]);
                    }),
                },
                new object[]
                {
                    new[] { 123L, 456L },
                    new Action<object>(result =>
                    {
                        Assert.IsAssignableFrom<IReadOnlyList<object>>(result);
                        Assert.Equal(2, ((IReadOnlyList<object>)result).Count);
                        Assert.Equal(123L, ((IReadOnlyList<object>)result)[0]);
                        Assert.Equal(456L, ((IReadOnlyList<object>)result)[1]);
                    }),
                },
                new object[]
                {
                    new[] { 123, 456 },
                    new Action<object>(result =>
                    {
                        Assert.IsAssignableFrom<IReadOnlyList<object>>(result);
                        Assert.Equal(2, ((IReadOnlyList<object>)result).Count);
                        Assert.Equal(123L, ((IReadOnlyList<object>)result)[0]);
                        Assert.Equal(456L, ((IReadOnlyList<object>)result)[1]);
                    }),
                },
                new object[]
                {
                    "string",
                    new Action<object>(result =>
                    {
                        Assert.IsType<string>(result);
                        Assert.Equal("string", result);
                    }),
                },
                new object[]
                {
                    123.456d,
                    new Action<object>(result =>
                    {
                        Assert.IsType<double>(result);
                        Assert.Equal(123.456d, result);
                    }),
                },
                new object[]
                {
                    123L,
                    new Action<object>(result =>
                    {
                        Assert.IsType<long>(result);
                        Assert.Equal(123L, result);
                    }),
                },
                new object[]
                {
                    123,
                    new Action<object>(result =>
                    {
                        Assert.IsType<long>(result);
                        Assert.Equal(123L, result);
                    }),
                },
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
}
