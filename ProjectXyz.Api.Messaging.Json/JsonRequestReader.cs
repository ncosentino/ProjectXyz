
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Json
{
    public sealed class JsonRequestReader : IRequestReader
    {
        #region Fields
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        #endregion

        #region Constructors
        private JsonRequestReader(JsonSerializerSettings jsonSerializerSettings)
        {
            _jsonSerializerSettings = jsonSerializerSettings;
        }
        #endregion

        #region Methods
        public static IRequestReader Create(JsonSerializerSettings jsonSerializerSettings)
        {
            var reader = new JsonRequestReader(jsonSerializerSettings);
            return reader;
        }
        
        public TRequest Read<TRequest>(Stream stream)
            where TRequest : IRequest
        {
            return (TRequest)Read(stream, typeof(TRequest));
        }

        public IRequest Read(Stream stream, Type type)
        {
            using (var reader = new StreamReader(stream))
            {
                return (IRequest)JsonConvert.DeserializeObject(
                    reader.ReadToEnd(),
                    type,
                    _jsonSerializerSettings);
            }
        }
        #endregion
    }
}
