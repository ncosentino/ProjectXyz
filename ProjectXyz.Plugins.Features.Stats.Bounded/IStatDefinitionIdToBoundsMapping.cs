﻿using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Bounded
{
    public interface IStatDefinitionIdToBoundsMapping
    {
        IIdentifier StatDefinitiondId { get; }

        IStatBounds StatBounds { get; }
    }
}
