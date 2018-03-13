using System;

namespace ProjectXyz.Api.Framework.Events
{
    public sealed class EventArgs<TData> : EventArgs
    {
        public EventArgs(TData data)
        {
            Data = data;
        }

        public TData Data { get; }
    }
}
