using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Filtering.Autofac
{
    public sealed class FilteringModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<AnyItemDefinitionIdentifierFilterHandler>()
               .SingleInstance();
            builder
                .RegisterBuildCallback(c =>
                {
                    var facade = c.Resolve<IAttributeValueMatchFacade>();
                    facade.Register(c.Resolve<AnyItemDefinitionIdentifierFilterHandler>().Matcher);
                });
        }
    }
}
