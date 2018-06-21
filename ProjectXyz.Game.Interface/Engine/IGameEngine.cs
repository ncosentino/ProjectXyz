using System.Threading;
using System.Threading.Tasks;

namespace ProjectXyz.Game.Interface.Engine
{
    public interface IAsyncGameEngine
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
