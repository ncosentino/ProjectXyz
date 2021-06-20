using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Game.Core
{
    public sealed class GameObjectRepository : IGameObjectRepository
    {
        private readonly Dictionary<IIdentifier, IGameObject> _cache;
        private readonly IAttributeFilterer _attributeFilterer;

        public GameObjectRepository(IAttributeFilterer attributeFilterer)
        {
            _cache = new Dictionary<IIdentifier, IGameObject>();
            _attributeFilterer = attributeFilterer;
        }

        public void Save(IGameObject gameObject)
        {
            var identifierBehavior = gameObject.GetOnly<IIdentifierBehavior>();
            var id = identifierBehavior.Id;
            _cache[id] = gameObject;
        }

        public void Clear()
        {
            _cache.Clear();
        }

        public IEnumerable<IGameObject> LoadAll() => _cache.Values;

        public IEnumerable<IGameObject> Load(IReadOnlyCollection<IFilterAttributeValue> filters)
        {
            var results = _attributeFilterer.Filter(
                _cache.Values,
                filters);
            return results;
        }
    }
}
