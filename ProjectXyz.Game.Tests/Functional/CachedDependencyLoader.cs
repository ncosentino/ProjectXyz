using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ProjectXyz.Game.Core.Autofac;

namespace ProjectXyz.Game.Tests.Functional
{
    public static class CachedDependencyLoader
    {
        private static readonly Lazy<IContainer> _container = new Lazy<IContainer>(
            () => Load());

        public static IContainer Container => _container.Value;

        private static IContainer Load()
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            var modules = moduleDiscoverer.Discover(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var container = dependencyContainerBuilder.Create(modules);
            return container;
        }
    }
}
