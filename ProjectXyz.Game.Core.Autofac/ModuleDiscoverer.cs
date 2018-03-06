using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac.Core;
using Module = Autofac.Module;

namespace ProjectXyz.Game.Core.Autofac
{
    public sealed class ModuleDiscoverer
    {
        public IEnumerable<IModule> Discover(string filePattern)
        {
            var modules = Directory
                .GetFiles(
                    AppDomain.CurrentDomain.BaseDirectory,
                    filePattern,
                    SearchOption.TopDirectoryOnly)
                .Select(Assembly.LoadFrom)
                .SelectMany(x => x
                    .GetTypes()
                    .Where(type =>
                        typeof(IModule).IsAssignableFrom(type) &&
                        !type.IsAbstract &&
                        !type.IsInterface))
                .Select(x => (Module) Activator.CreateInstance(x));
            return modules;
        }
    }
}
