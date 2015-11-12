
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using ProjectXyz.Api.Messaging.Interface;
using ProjectXyz.Api.Messaging.Serialization.Interface;

namespace ProjectXyz.Api.Messaging.Serialization.Json
{
    public sealed class JsonResponseReader : IResponseReader
    {
        #region Constructors
        private JsonResponseReader()
        {
        }
        #endregion

        #region Methods
        public static IResponseReader Create()
        {
            var reader = new JsonResponseReader();
            return reader;
        }
        
        public TResponse Read<TResponse>(Stream stream)
            where TResponse : IResponse
        {
            return (TResponse)Read(stream, typeof(TResponse));
        }

        public IResponse Read(Stream stream, Type type)
        {
            using (var reader = new StreamReader(stream))
            {
                return (IResponse)JsonConvert.DeserializeObject(
                    reader.ReadToEnd(),
                    type);
            }
        }
        #endregion
    }
}
