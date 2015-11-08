using System.Threading;
using System.Threading.Tasks;

namespace ProjectXyz.Game.Core
{
    public interface IGame
    {
        #region Methods
        Task StartAsync(CancellationToken cancellationToken);
        #endregion
    }
}