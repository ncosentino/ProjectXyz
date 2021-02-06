using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Autofac;

using ProjectXyz.Api.Data.Serialization;
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
            var scopeFactory = new TestLifeTimeScopeFactory();
            var scope = scopeFactory.CreateScope();
            _serializer = scope.Resolve<ISerializer>();
            _deserializer = scope.Resolve<IDeserializer>();
        }

        [ClassData(typeof(TestData))]
        [Theory]
        private void Deserialize_SerializedData_ExpectedResult(
            object dataToSerialize,
            Action<object, object> validateCallback)
        {
            var serializedStream = new MemoryStream();
            _serializer.Serialize(
                serializedStream,
                dataToSerialize,
                Encoding.UTF8);
            serializedStream.Seek(0, SeekOrigin.Begin);
            var deserialized = _deserializer.Deserialize(serializedStream);
            validateCallback(dataToSerialize, deserialized);
        }

        private sealed class TestData : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    new ObjWithOnePropertiesTwoConstructorParameter("constructor parameter", "unused"),
                    new Action<object, object>((original, result) =>
                    {
                        Assert.IsType<ObjWithOnePropertiesTwoConstructorParameter>(result);
                        Assert.Equal("constructor parameter", ((ObjWithOnePropertiesTwoConstructorParameter)result).Property1);
                    }),
                },
                new object[]
                {
                    new ObjWithTwoPropertiesOneConstructorParameter("constructor parameter"),
                    new Action<object, object>((original, result) =>
                    {
                        Assert.IsType<ObjWithTwoPropertiesOneConstructorParameter>(result);
                        Assert.Equal("constructor parameter", ((ObjWithTwoPropertiesOneConstructorParameter)result).Property1);
                        Assert.Null(((ObjWithTwoPropertiesOneConstructorParameter)result).Property2);
                    }),
                },
                new object[]
                {
                    new ObjWithTwoPropertiesOneConstructorParameterSecondAsReadonly("constructor parameter"),
                    new Action<object, object>((original, result) =>
                    {
                        Assert.IsType<ObjWithTwoPropertiesOneConstructorParameterSecondAsReadonly>(result);
                        Assert.Equal("constructor parameter", ((ObjWithTwoPropertiesOneConstructorParameterSecondAsReadonly)result).Property1);
                        Assert.NotNull(((ObjWithTwoPropertiesOneConstructorParameterSecondAsReadonly)result).Property2);
                        Assert.NotEqual(
                            ((ObjWithTwoPropertiesOneConstructorParameterSecondAsReadonly)original).Property2,
                            ((ObjWithTwoPropertiesOneConstructorParameterSecondAsReadonly)result).Property2);
                    }),
                },
                new object[]
                {
                    new RepeatedPropertyRefObject()
                    {
                        Property1 = SingletonObject.Value,
                        Property2 = SingletonObject.Value,
                    },
                    new Action<object, object>((original, result) =>
                    {
                        Assert.IsType<RepeatedPropertyRefObject>(result);
                        Assert.NotNull(((RepeatedPropertyRefObject)result).Property1);
                        Assert.NotNull(((RepeatedPropertyRefObject)result).Property2);
                    }),
                },
                new object[]
                {
                    new[] { SingletonObject.Value, SingletonObject.Value },
                    new Action<object, object>((original, result) =>
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
                    new Action<object, object>((original, result) =>
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
                    new Action<object, object>((original, result) =>
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
                    new Action<object, object>((original, result) =>
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
                    new Action<object, object>((original, result) =>
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
                    new Action<object, object>((original, result) =>
                    {
                        Assert.IsType<string>(result);
                        Assert.Equal("string", result);
                    }),
                },
                new object[]
                {
                    123.456d,
                    new Action<object, object>((original, result) =>
                    {
                        Assert.IsType<double>(result);
                        Assert.Equal(123.456d, result);
                    }),
                },
                new object[]
                {
                    123L,
                    new Action<object, object>((original, result) =>
                    {
                        Assert.IsType<long>(result);
                        Assert.Equal(123L, result);
                    }),
                },
                new object[]
                {
                    123,
                    new Action<object, object>((original, result) =>
                    {
                        Assert.IsType<long>(result);
                        Assert.Equal(123L, result);
                    }),
                },
            };

            public sealed class ObjWithOnePropertiesTwoConstructorParameter
            {
                public ObjWithOnePropertiesTwoConstructorParameter(string property1, string property2)
                {
                    Property1 = property1;
                }

                public string Property1 { get; }
            }

            public sealed class ObjWithTwoPropertiesOneConstructorParameter
            {
                public ObjWithTwoPropertiesOneConstructorParameter(string property1)
                {
                    Property1 = property1;
                }

                public string Property1 { get; }

                public string Property2 { get; }
            }

            public sealed class ObjWithTwoPropertiesOneConstructorParameterSecondAsReadonly
            {
                public ObjWithTwoPropertiesOneConstructorParameterSecondAsReadonly(string property1)
                {
                    Property1 = property1;
                }

                public string Property1 { get; }

                public string Property2 { get; } = Guid.NewGuid().ToString();
            }

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
