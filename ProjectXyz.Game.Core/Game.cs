using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProjectXyz.Api.Interface;
using ProjectXyz.Game.Core.Binding;
using ProjectXyz.Game.Interface;

namespace ProjectXyz.Game.Core
{
    public sealed class Game : IGame
    {
        #region Fields
        private readonly IApiManager _apiManager;
        private readonly IGameManager _gameManager;
        #endregion

        #region Constructors
        private Game(
            IGameManager gameManager,
            IApiManager apiManager)
        {
            _gameManager = gameManager;
            _apiManager = apiManager;


        }
        #endregion

        #region Methods
        public static IGame Create(
            IGameManager gameManager,
            IApiManager apiManager)
        {
            var game = new Game(
                gameManager,
                apiManager);
            return game;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (MapApiBinder.Create(_apiManager, _gameManager.ApplicationManager.Maps))
            {
                await Task.Delay(-1, cancellationToken);
            }
        }
        #endregion
    }
}
