using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Filtering.Autofac
{
    public sealed class FilteringModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<AllTagsFilterHandler>()
               .SingleInstance();
            builder
               .RegisterType<AnyTagsFilterHandler>()
               .SingleInstance();
            builder
               .RegisterType<AnyIdFilterHandler>()
               .SingleInstance();
            builder
               .RegisterType<PredicateFilterHandler>()
               .SingleInstance();
            builder
                .RegisterBuildCallback(c =>
                {
                    var facade = c.Resolve<IAttributeValueMatchFacade>();
                    facade.Register(c.Resolve<AllTagsFilterHandler>().Matcher);
                    facade.Register(c.Resolve<AnyTagsFilterHandler>().Matcher);
                    facade.Register(c.Resolve<AnyIdFilterHandler>().Matcher);
                    facade.Register(c.Resolve<PredicateFilterHandler>().Matcher);
                });
        }
    }
}
