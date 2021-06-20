using System.Threading.Tasks;

namespace ProjectXyz.Api.Systems
{
    public interface ISystem
    {
        Task UpdateAsync(ISystemUpdateContext systemUpdateContext);
    }
}