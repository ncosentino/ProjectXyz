using System;
using System.Reflection;

namespace ProjectXyz.Game.Core.Autofac
{
    public sealed class AssemblyLoadedEventArgs : EventArgs
    {
        public AssemblyLoadedEventArgs(Assembly assembly)
        {
            Assembly = assembly;
        }

        public Assembly Assembly { get; }
    }
}