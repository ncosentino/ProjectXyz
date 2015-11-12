using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using ProjectXyz.Api.Messaging.Interface;
using ProjectXyz.Api.Messaging.Serialization.Interface;

namespace ProjectXyz.Api.Messaging.Serialization.Json
{
    public sealed class JsonRequestWriter : IRequestWriter
    {
        #region Constructors
        private JsonRequestWriter()
        {
        }
        #endregion

        #region Methods
        public static IRequestWriter Create()
        {
            var writer = new JsonRequestWriter();
            return writer;
        }

        public void Write<TRequest>(TRequest request, Stream stream)
            where TRequest : IRequest
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(JsonConvert.SerializeObject(request));
            }
        }
        #endregion
    }
}
