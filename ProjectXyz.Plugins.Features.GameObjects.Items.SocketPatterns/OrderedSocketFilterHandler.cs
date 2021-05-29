using System;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns
{
    public sealed class OrderedSocketFilterHandler
    {
        public OrderedSocketFilterHandler(Lazy<IAttributeFilterer> lazyAttributeFilterer)
        {
            Matcher = (filter, item) =>
            {
                if (!item.TryGetFirst<ICanBeSocketedBehavior>(out var canBeSocketed))
                {
                    return false;
                }

                if (filter.PerSocketFilters.Count != canBeSocketed.OccupiedSockets.Count)
                {
                    return false;
                }

                using (var socketFilterEnumerator = filter.PerSocketFilters.GetEnumerator())
                using (var occupiedSocketEnumerator = canBeSocketed.OccupiedSockets.GetEnumerator())
                {
                    while (socketFilterEnumerator.MoveNext() && occupiedSocketEnumerator.MoveNext())
                    {
                        var socketFilter = socketFilterEnumerator.Current;
                        var occupiedSocket = occupiedSocketEnumerator.Current;

                        if (!lazyAttributeFilterer.Value.IsMatch(
                            occupiedSocket.Owner,
                            socketFilter))
                        {
                            return false;
                        }
                    }
                }

                return true;
            };
        }

        public GenericAttributeValueMatchDelegate<OrderedSocketFilter, IGameObject> Matcher;
    }
}
