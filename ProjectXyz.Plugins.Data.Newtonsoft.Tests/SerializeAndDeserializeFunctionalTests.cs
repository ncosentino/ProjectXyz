using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Autofac;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;
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
            var scopeFactory = new TestLifeTimeScopeFactory();
            var scope = scopeFactory.CreateScope();
            _serializer = scope.Resolve<ISerializer>();
            _deserializer = scope.Resolve<IDeserializer>();
        }

        [ClassData(typeof(TestData))]
        [Theory]
        private void Deserialize_SerializedData_ExpectedResult(
            object dataToSerialize,
            Action<object, object, string> validateCallback)
        {
            var serializedStream = new MemoryStream();
            _serializer.Serialize(
                serializedStream,
                dataToSerialize,
                Encoding.UTF8);
            serializedStream.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(serializedStream))
            {
                var json = reader.ReadToEnd();
                serializedStream.Seek(0, SeekOrigin.Begin);
             
                var deserialized = _deserializer.Deserialize(serializedStream);
                validateCallback(dataToSerialize, deserialized, json);
            }
        }

        private sealed class TestData : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    new ObjWithOnePropertiesTwoConstructorParameter("constructor parameter", "unused"),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsType<ObjWithOnePropertiesTwoConstructorParameter>(result);
                        Assert.Equal("constructor parameter", ((ObjWithOnePropertiesTwoConstructorParameter)result).Property1);
                    }),
                },
                new object[]
                {
                    new ObjWithTwoPropertiesOneConstructorParameter("constructor parameter"),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsType<ObjWithTwoPropertiesOneConstructorParameter>(result);
                        Assert.Equal("constructor parameter", ((ObjWithTwoPropertiesOneConstructorParameter)result).Property1);
                        Assert.Null(((ObjWithTwoPropertiesOneConstructorParameter)result).Property2);
                    }),
                },
                new object[]
                {
                    new ObjWithTwoPropertiesOneConstructorParameterSecondAsReadonly("constructor parameter"),
                    new Action<object, object, string>((original, result, json) =>
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
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsType<RepeatedPropertyRefObject>(result);

                        // NOTE: these singleton objects are TRULY non constructable so... we can't get them back
                        Assert.Null(((RepeatedPropertyRefObject)result).Property1);
                        Assert.Null(((RepeatedPropertyRefObject)result).Property2);
                        Assert.NotNull(json);
                        Assert.NotEmpty(json);
                    }),
                },
                new object[]
                {
                    new RepeatedSingletionViaConstructor(SingletonObject.Value, SingletonObject.Value),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsType<RepeatedSingletionViaConstructor>(result);
                    
                        // NOTE: these singleton objects are TRULY non constructable so... we can't get them back
                        Assert.Null(((RepeatedSingletionViaConstructor)result).Property1);
                        Assert.Null(((RepeatedSingletionViaConstructor)result).Property2);
                        Assert.NotNull(json);
                        Assert.NotEmpty(json);
                    }),
                },
                new object[]
                {
                    new[] { SingletonObject.Value, SingletonObject.Value },
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<IReadOnlyList<object>>(result);
                        Assert.Equal(2, ((IReadOnlyList<object>)result).Count);

                        // NOTE: these singleton objects are TRULY non constructable so... we can't get them back
                        Assert.Null(((IReadOnlyList<object>)result).First());
                        Assert.Null(((IReadOnlyList<object>)result).Last());
                        Assert.NotNull(json);
                        Assert.NotEmpty(json);
                    }),
                },
                new object[]
                {
                    new ObjWithBackpointerAndOneConstructorParameter(new ObjWithBackpointerAndOneConstructorParameter(null)),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<ObjWithBackpointerAndOneConstructorParameter>(result);
                        Assert.Equal(result, ((ObjWithBackpointerAndOneConstructorParameter)result).BackpointerProperty);
                        Assert.IsAssignableFrom<ObjWithBackpointerAndOneConstructorParameter>(((ObjWithBackpointerAndOneConstructorParameter)result).Child);
                        Assert.Equal(
                            ((ObjWithBackpointerAndOneConstructorParameter)result).Child,
                            ((ObjWithBackpointerAndOneConstructorParameter)result).Child.BackpointerProperty);
                        Assert.Null(((ObjWithBackpointerAndOneConstructorParameter)result).Child.Child);
                    }),
                },
                new object[]
                {
                    new ObjWithTwoDivergentConstructors(123),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<ObjWithTwoDivergentConstructors>(result);
                        Assert.Equal(123, ((ObjWithTwoDivergentConstructors)result).Property2);
                    }),
                },
                new object[]
                {
                    new ObjWithTwoDivergentConstructors("test"),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<ObjWithTwoDivergentConstructors>(result);
                        Assert.Equal("test", ((ObjWithTwoDivergentConstructors)result).Property1);
                    }),
                },
                new object[]
                {
                    new ObjWithBackpointerAndNoConstructor(),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<ObjWithBackpointerAndNoConstructor>(result);
                    }),
                },
                new object[]
                {
                    new ObjectWithIdentifier(new StringIdentifier("test")),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<ObjectWithIdentifier>(result);
                        Assert.IsAssignableFrom<StringIdentifier>(((ObjectWithIdentifier)result).Property);
                        Assert.Equal("test", ((StringIdentifier)((ObjectWithIdentifier)result).Property).Identifier);
                    }),
                },
                new object[]
                {
                    new ObjectWithIdentifier(new IntIdentifier(123)),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<ObjectWithIdentifier>(result);
                        Assert.IsAssignableFrom<IntIdentifier>(((ObjectWithIdentifier)result).Property);
                        Assert.Equal(123, ((IntIdentifier)((ObjectWithIdentifier)result).Property).Identifier);
                    }),
                },
                new object[]
                {
                    new ObjectWithIntArrayInputReadOnlyCollectionProperty(new[] { 1, 2, 3 }),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<ObjectWithIntArrayInputReadOnlyCollectionProperty>(result);

                        // ensure the collection primitives are treates as such
                        Assert.Contains("\"Data\":[1,2,3]", json);
                    }),
                },
                new object[]
                {
                    new ObjectWithEnumerableKvpInputReadOnlyDictionaryProperty(new Dictionary<string, int>()
                    {
                        ["hello"] = 123,
                        ["world"] = 456,
                    }),
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.Contains(
                            "{\"hello\":123,\"world\":456}",
                            json);

                        Assert.IsAssignableFrom<ObjectWithEnumerableKvpInputReadOnlyDictionaryProperty>(result);

                        var castedResult = Assert.IsAssignableFrom<ObjectWithEnumerableKvpInputReadOnlyDictionaryProperty>(result);
                        Assert.Equal(2, castedResult.Property.Count);
                        Assert.Contains("hello", castedResult.Property.Keys);
                        Assert.Equal(123, castedResult.Property["hello"]);
                        Assert.Contains("world", castedResult.Property.Keys);
                        Assert.Equal(456, castedResult.Property["world"]);
                    }),
                },
                new object[]
                {
                    new Dictionary<string, int>()
                    {
                        ["hello"] = 123,
                        ["world"] = 456,
                    },
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.Contains(
                            "{\"hello\":123,\"world\":456}",
                            json);

                        var dictionaryResult = Assert.IsAssignableFrom<IReadOnlyDictionary<string, int>>(result);
                        Assert.Equal(2, dictionaryResult.Count);
                        Assert.Contains("hello", dictionaryResult.Keys);
                        Assert.Equal(123, dictionaryResult["hello"]);
                        Assert.Contains("world", dictionaryResult.Keys);
                        Assert.Equal(456, dictionaryResult["world"]);
                    }),
                },
                new object[]
                {
                    new[] { "test", "array" },
                    new Action<object, object, string>((original, result, json) =>
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
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<IReadOnlyCollection<object>>(result);
                        Assert.Equal(2, ((IReadOnlyList<object>)result).Count);
                        Assert.Equal(12.3d, ((IReadOnlyList<object>)result)[0]);
                        Assert.Equal(45.6d, ((IReadOnlyList<object>)result)[1]);
                    }),
                },
                new object[]
                {
                    new[] { 123L, 456L }, // FIXME: this is technically wrong if these are longs
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<IReadOnlyList<object>>(result);
                        Assert.Equal(2, ((IReadOnlyList<object>)result).Count);
                        Assert.Equal(123, ((IReadOnlyList<object>)result)[0]);
                        Assert.Equal(456, ((IReadOnlyList<object>)result)[1]);
                    }),
                },
                new object[]
                {
                    new[] { 123, 456 },
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsAssignableFrom<IReadOnlyList<object>>(result);
                        Assert.Equal(2, ((IReadOnlyList<object>)result).Count);
                        Assert.Equal(123, ((IReadOnlyList<object>)result)[0]);
                        Assert.Equal(456, ((IReadOnlyList<object>)result)[1]);
                    }),
                },
                new object[]
                {
                    "string",
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsType<string>(result);
                        Assert.Equal("string", result);
                    }),
                },
                new object[]
                {
                    123.456d,
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsType<double>(result);
                        Assert.Equal(123.456d, result);
                    }),
                },
                new object[]
                {
                    123L, // FIXME: this is technically wrong if these are longs
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsType<int>(result);
                        Assert.Equal(123, result);
                    }),
                },
                new object[]
                {
                    123,
                    new Action<object, object, string>((original, result, json) =>
                    {
                        Assert.IsType<int>(result);
                        Assert.Equal(123, result);
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

            private sealed class ObjWithBackpointerAndNoConstructor
            {
                public ObjWithBackpointerAndNoConstructor BackpointerProperty => this;
            }

            private sealed class ObjWithBackpointerAndOneConstructorParameter
            {
                public ObjWithBackpointerAndOneConstructorParameter(ObjWithBackpointerAndOneConstructorParameter child)
                {
                    Child = child;
                }

                public ObjWithBackpointerAndOneConstructorParameter BackpointerProperty => this;

                public ObjWithBackpointerAndOneConstructorParameter Child { get; }
            }

            private sealed class RepeatedPropertyRefObject
            {
                public SingletonObject Property1 { get; set; }

                public SingletonObject Property2 { get; set; }
            }

            private sealed class RepeatedSingletionViaConstructor
            {
                public RepeatedSingletionViaConstructor(
                    SingletonObject property1,
                    SingletonObject property2)
                {
                    Property1 = property1;
                    Property2 = property2;
                }

                public SingletonObject Property1 { get; }

                public SingletonObject Property2 { get; }
            }

            public sealed class ObjWithTwoDivergentConstructors
            {
                public ObjWithTwoDivergentConstructors(string property1)
                {
                    Property1 = property1;
                }

                public ObjWithTwoDivergentConstructors(int property2)
                {
                    Property2 = property2;
                }

                public string Property1 { get; }

                public int Property2 { get; }
            }

            public sealed class ObjectWithIdentifier
            {
                public ObjectWithIdentifier(IIdentifier property)
                {
                    Property = property;
                }

                public IIdentifier Property { get; }
            }

            public sealed class ObjectWithIntArrayInputReadOnlyCollectionProperty
            {
                public ObjectWithIntArrayInputReadOnlyCollectionProperty(IEnumerable<int> property)
                {
                    Property = property.ToArray();
                }

                public IReadOnlyCollection<int> Property { get; }
            }

            public sealed class ObjectWithEnumerableKvpInputReadOnlyDictionaryProperty
            {
                public ObjectWithEnumerableKvpInputReadOnlyDictionaryProperty(IEnumerable<KeyValuePair<string, int>> property)
                {
                    Property = property.ToDictionary(x => x.Key, x => x.Value);
                }

                public IReadOnlyDictionary<string, int> Property { get; }
            }

            private sealed class SingletonObject
            {
                private static Lazy<SingletonObject> _lazySingle = new Lazy<SingletonObject>(() => new SingletonObject());

                public static SingletonObject Value => _lazySingle.Value;

                private SingletonObject()
                {
                }
            }

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
