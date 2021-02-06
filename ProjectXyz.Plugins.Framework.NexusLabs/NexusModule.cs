using Autofac;

using NexusLabs.Framework;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Framework.NexusLabs
{
    public class NexusModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<Cast>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .Register(x => new Random(new System.Random()))
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}
