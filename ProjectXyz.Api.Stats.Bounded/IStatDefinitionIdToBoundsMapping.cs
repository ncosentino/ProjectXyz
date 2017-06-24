using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.Stats.Bounded
{
    public interface IStatDefinitionIdToBoundsMapping
    {
        IIdentifier StatDefinitiondId { get; }

        IStatBounds StatBounds { get; }
    }
}
