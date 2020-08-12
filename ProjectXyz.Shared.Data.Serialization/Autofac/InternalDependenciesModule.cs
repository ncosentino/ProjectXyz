using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Autofac
{
    public sealed class InternalDependenciesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
        }
    }
}
