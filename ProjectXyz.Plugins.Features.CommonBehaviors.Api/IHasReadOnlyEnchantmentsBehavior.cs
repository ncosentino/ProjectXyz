﻿using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasReadOnlyEnchantmentsBehavior : IBehavior
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}