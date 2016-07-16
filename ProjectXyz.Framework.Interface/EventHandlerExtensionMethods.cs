using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Framework.Interface
{
    public static class EventHandlerExtensionMethods
    {
        public static void InvokeIfExists<TEventArgs>(
            this EventHandler<TEventArgs> handler,
            object sender,
            TEventArgs eventArgs)
        {
            handler?.Invoke(
                sender, 
                eventArgs);
        }

        public static void InvokeIfExists<TEventArgs>(
            this EventHandler<TEventArgs> handler,
            object sender,
            Func<TEventArgs> createArgsCallback)
        {
            handler?.Invoke(
                sender, 
                createArgsCallback());
        }

        public static async Task InvokeIfExistsAsync<TEventArgs>(
            this EventHandler<TEventArgs> handler,
            object sender,
            Func<Task<TEventArgs>> createArgsAsyncCallback)
        {
            handler?.Invoke(
                sender,
                await createArgsAsyncCallback());
        }

        public static void InvokeIfExists(
            this EventHandler<EventArgs> handler,
            object sender)
        {
            handler?.Invoke(
                sender,
                EventArgs.Empty);
        }
    }
}
