using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class SocketBonusGeneratorComponent : IGeneratorComponent
    {
        public SocketBonusGeneratorComponent(
            IEnumerable<IFilterAttribute> filters,
            IEnumerable<IIdentifier> enchantmentDefinitionIds)
        {
            Filters = filters.ToArray();
            EnchantmentDefinitionIds = enchantmentDefinitionIds.ToArray();
        }

        public IReadOnlyCollection<IFilterAttribute> Filters { get; }

        public IReadOnlyCollection<IIdentifier> EnchantmentDefinitionIds { get; }
    }
}