using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Game.Api
{
    public interface IGameObjectRepository
    {
        IEnumerable<IGameObject> Load(IReadOnlyCollection<IFilterAttributeValue> filters);

        void Save(IGameObject gameObject);

        // FIXME: this API feels ridiculously weird... we need to have this
        // associated with some sort of current game state so it doesn't feel
        // like we're potentially blowing away game content
        void Clear();
    }
}