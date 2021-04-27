using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Game.Api
{
    public interface IGameObjectRepository
    {
        IEnumerable<IGameObject> LoadAll();

        IEnumerable<IGameObject> Load(IFilterContext filterContext);

        void Save(IGameObject gameObject);
    }
}