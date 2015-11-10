using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectXyz.Game.Interface
{
    public interface IGame
    {
        #region Events
        event EventHandler<EventArgs> Started;
        #endregion

        #region Methods
        Task StartAsync(CancellationToken cancellationToken);
        #endregion
    }
}