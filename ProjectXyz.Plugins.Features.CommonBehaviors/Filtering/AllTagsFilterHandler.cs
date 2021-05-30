﻿using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Filtering
{
    public sealed class AllTagsFilterHandler
    {
        public AllTagsFilterHandler()
        {
            Matcher = (filter, item) =>
            {
                if (!item.TryGetFirst<ITagsBehavior>(out var tagsBehavior))
                {
                    return false;
                }

                var match = filter.Tags.All(t1 => tagsBehavior.Tags.Any(t2 => t1.Equals(t2)));
                return match;
            };
        }

        public GenericAttributeValueMatchDelegate<AllTagsFilter, IGameObject> Matcher;
    }
}