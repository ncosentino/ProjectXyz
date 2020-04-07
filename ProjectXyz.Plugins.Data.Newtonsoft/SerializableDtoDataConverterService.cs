using System;
using System.Collections.Generic;
using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class SerializableDtoDataConverterService : ISerializableDtoDataConverterService
    {
        private static readonly Lazy<ISerializableDtoDataConverterService> LAZY_INSTANCE = new Lazy<ISerializableDtoDataConverterService>(
            () => new SerializableDtoDataConverterService());

        private readonly Dictionary<string, ISerializableDtoDataConverter> _mapping;

        private SerializableDtoDataConverterService()
        {
            _mapping = new Dictionary<string, ISerializableDtoDataConverter>();
        }

        public static ISerializableDtoDataConverterService Instance => LAZY_INSTANCE.Value;

        public void Register(
            string type,
            ISerializableDtoDataConverter serializableDtoDataConverter)
        {
            _mapping.Add(type, serializableDtoDataConverter);
        }

        public bool TryGet(
            string name,
            out ISerializableDtoDataConverter serializableDtoDataConverter)
        {
            return _mapping.TryGetValue(
                name,
                out serializableDtoDataConverter);
        }
    }
}