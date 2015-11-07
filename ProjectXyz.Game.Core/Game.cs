using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Interface;
using ProjectXyz.Game.Interface;

namespace ProjectXyz.Game.Core
{
    public sealed class Game
    {
        #region Fields
        private readonly IApiManager _apiManager;
        private readonly IGameManager _gameManager;
        #endregion
    }
}
