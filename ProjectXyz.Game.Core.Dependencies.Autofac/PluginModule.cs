using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class PluginModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assemblies = Directory
                .GetFiles(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "*Plugin*.dll",
                    SearchOption.TopDirectoryOnly)
                .Select(Assembly.LoadFrom)
                .ToArray();
            builder.RegisterAssemblyModules(assemblies);
        }
    }
}
