using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace ProjectXyz.Game.Core.Autofac
{
    public sealed class DependencyContainerBuilder
    {
        public IContainer Create(IEnumerable<IModule> modules)
        {
            var containerBuilder = new ContainerBuilder();

            foreach (var module in modules)
            {
                containerBuilder.RegisterModule(module);
            }

            var container = containerBuilder.Build();
            return container;
        }
    }
}
