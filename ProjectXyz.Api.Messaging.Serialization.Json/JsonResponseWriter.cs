using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ProjectXyz.Api.Messaging.Interface;
using ProjectXyz.Api.Messaging.Serialization.Interface;

namespace ProjectXyz.Api.Messaging.Serialization.Json
{
    public sealed class JsonResponseWriter : IResponseWriter
    {
        #region Constructors
        private JsonResponseWriter()
        {
        }
        #endregion

        #region Methods
        public static IResponseWriter Create()
        {
            var writer = new JsonResponseWriter();
            return writer;
        }

        public void Write<TResponse>(TResponse response, Stream stream)
            where TResponse : IResponse
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(JsonConvert.SerializeObject(response));
            }
        }
        #endregion
    }
}
