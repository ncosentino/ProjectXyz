using System.Threading;
using System.Threading.Tasks;

namespace ProjectXyz.Game.Interface.Engine
{
    public interface IGameEngine
    {
        Task Start(CancellationToken cancellationToken);
    }
}
