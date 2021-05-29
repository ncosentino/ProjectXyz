using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

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
            builder
               .RegisterType<TransformativeSocketPatternHandler>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<TransformativeSocketPatternRepositoryFacade>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<OrderedSocketFilterHandler>()
               .SingleInstance();
            builder
                .RegisterBuildCallback(c =>
                {
                    var facade = c.Resolve<IAttributeValueMatchFacade>();
                    facade.Register(c.Resolve<OrderedSocketFilterHandler>().Matcher);
                });
        }
    }
}