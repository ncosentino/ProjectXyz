using System;

namespace ProjectXyz.Api.Amqp
{
    public interface IChannelWriter
    {
        void WriteToChannel(
            string type, 
            byte[] bytes);

        void WriteToChannel(
            string type,
            byte[] bytes, 
            Guid responseId);
    }
}