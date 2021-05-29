﻿using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ITagsBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> Tags { get; }
    }
}