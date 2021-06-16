using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Game.Api
{
    public interface IGameObjectRepository
    {
        IEnumerable<IGameObject> LoadAll();

        IEnumerable<IGameObject> Load(IFilterContext filterContext);

        void Save(IGameObject gameObject);

        // FIXME: this API feels ridiculously weird... we need to have this
        // associated with some sort of current game state so it doesn't feel
        // like we're potentially blowing away game content
        void Clear();
    }
}