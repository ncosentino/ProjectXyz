using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Application.Interface.Worlds;

namespace ProjectXyz.Application.Core.Worlds
{
    public sealed class World : IWorld
    {
        #region Fields
        private readonly List<IMap> _maps;
        #endregion

        #region Constructors
        private World()
        {
            _maps = new List<IMap>();
        }
        #endregion

        #region Properties
        public IEnumerable<IMap> Maps => _maps;
        #endregion

        #region Methods
        public static IWorld Create()
        {
            var world = new World();
            return world;
        }

        public void ActivateMap(IMap map)
        {
            _maps.Add(map);
        }
        #endregion
    }
}
