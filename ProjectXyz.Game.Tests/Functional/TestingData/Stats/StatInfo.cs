using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Stats
{
    public sealed class StatInfo
    {
        public StatDefinitionIds DefinitionIds { get; } = new StatDefinitionIds();

        public sealed class StatDefinitionIds
        {
            public IIdentifier StatA { get; } = new StringIdentifier("Stat A");
            public IIdentifier StatB { get; } = new StringIdentifier("Stat B");
            public IIdentifier StatC { get; } = new StringIdentifier("Stat C");
        }
    }
}
