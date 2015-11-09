using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Application.Interface.Worlds;

namespace ProjectXyz.Application.Core.Worlds
{
    public sealed class WorldManager : IWorldManager
    {
        #region Constructors
        private WorldManager()
        {
            World = Worlds.World.Create();
        }
        #endregion

        #region Properties
        public IWorld World { get; }
        #endregion

        #region Methods
        public static IWorldManager Create()
        {
            var manager = new WorldManager();
            return manager;
        }
        #endregion
    }
}
