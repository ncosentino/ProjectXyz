using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Stats;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Plugins.Features.BoundedStats.Api;
using ProjectXyz.Shared.Framework;

namespace ConsoleApplication1.Modules
{
    public sealed class DummyDependencies : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<StateIdToTermRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatDefinitionToTermMappingRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ValueMapperRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class ValueMapperRepository : IValueMapperRepository
    {
        public IEnumerable<ValueMapperDelegate> GetValueMappers()
        {
            yield break;
        }
    }

    public sealed class StateIdToTermRepo : IStateIdToTermRepository
    {
        public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
        {
            yield break;
        }
    }

    public sealed class StatDefinitionToTermMappingRepo : IStatDefinitionToTermMappingRepository
    {
        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings()
        {
            yield return new StatDefinitionToTermMapping(
                new StringIdentifier("stat1"),
                "stat1");
            yield return new StatDefinitionToTermMapping(
                new StringIdentifier("stat2"),
                "stat2");
        }
    }
}
