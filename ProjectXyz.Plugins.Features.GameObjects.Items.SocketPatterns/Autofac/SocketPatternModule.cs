using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Autofac
{
    public sealed class SocketPatternModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<SocketPatternHandlerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SocketableInfoFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}