using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac.Core;

namespace ProjectXyz.Game.Core.Autofac
{
    public sealed class ModuleDiscoverer : IModuleDiscoverer
    {
        public event EventHandler<AssemblyLoadFailedEventArgs> AssemblyLoadFailed;

        public event EventHandler<AssemblyLoadedEventArgs> AssemblyLoaded;

        public IEnumerable<IModule> Discover(Assembly assembly)
        {
            AssemblyLoaded?.Invoke(this, new AssemblyLoadedEventArgs(assembly));
            IReadOnlyCollection<Type> types;

            try
            {
                types = assembly.GetTypes();
            }
            catch
            {
                return Enumerable.Empty<IModule>();
            }

            var modules = types
                .Where(type =>
                    typeof(IModule).IsAssignableFrom(type) &&
                    !type.IsAbstract &&
                    !type.IsInterface)
                .Select(x => (IModule)Activator.CreateInstance(x));
            return modules;
        }

        public IEnumerable<IModule> Discover(
            string moduleDirectory,
            string filePattern)
        {
            var modules = Directory
                .GetFiles(
                    moduleDirectory,
                    filePattern,
                    SearchOption.TopDirectoryOnly)
                .Select(assemblypath =>
                {
                    try
                    {
                        return Assembly.LoadFrom(assemblypath);
                    }
                    catch (Exception ex) when (AssemblyLoadFailed != null)
                    {
                        var args = new AssemblyLoadFailedEventArgs(
                            assemblypath,
                            ex);
                        AssemblyLoadFailed.Invoke(this, args);
                        if (!args.Handled)
                        {
                            throw;
                        }

                        return null;
                    }
                })
                .Where(x => x != null)
                .SelectMany(Discover);
            return modules;
        }
    }
}
