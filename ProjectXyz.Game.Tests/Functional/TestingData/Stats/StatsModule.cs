﻿using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Stats
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatDefinitionToTermMappingRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => new StatDefinitionIdToBoundsMappingRepository(c.Resolve<TestData>()))
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class StatDefinitionToTermMappingRepo : IReadOnlyStatDefinitionToTermMappingRepository
        {
            private readonly StatInfo _statInfo;

            public StatDefinitionToTermMappingRepo()
            {
                _statInfo = new StatInfo();
            }

            public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings()
            {
                yield return new StatDefinitionToTermMapping() { StatDefinitionId = _statInfo.DefinitionIds.StatA, Term = "STAT_A" };
                yield return new StatDefinitionToTermMapping() { StatDefinitionId = _statInfo.DefinitionIds.StatB, Term = "STAT_B" };
                yield return new StatDefinitionToTermMapping() { StatDefinitionId = _statInfo.DefinitionIds.StatC, Term = "STAT_C" };
            }

            private sealed class StatDefinitionToTermMapping : IStatDefinitionToTermMapping
            {
                public IIdentifier StatDefinitionId { get; set; }

                public string Term { get; set; }
            }
        }
    }
}