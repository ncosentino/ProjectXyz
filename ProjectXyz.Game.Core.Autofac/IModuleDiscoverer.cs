using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac.Core;

namespace ProjectXyz.Game.Core.Autofac
{
    public interface IModuleDiscoverer
    {
        event EventHandler<AssemblyLoadFailedEventArgs> AssemblyLoadFailed;

        event EventHandler<AssemblyLoadedEventArgs> AssemblyLoaded;

        IEnumerable<IModule> Discover(Assembly assembly);

        IEnumerable<IModule> Discover(
            string moduleDirectory,
            string filePattern);
    }
}