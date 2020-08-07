using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using ProjectXyz.Game.Core.Autofac;

namespace ProjectXyz.Testing
{
    public sealed class TestLifeTimeScopeFactory
    {
        public ILifetimeScope CreateScope()
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            var moduleDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var modules = moduleDiscoverer
                .Discover(moduleDirectory, "*.exe")
                .Concat(moduleDiscoverer
                .Discover(moduleDirectory, "*.dll"));
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);
            var scope = dependencyContainer.BeginLifetimeScope();
            return scope;
        }
    }
}
