using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Systems;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeatherSystem : ISystem
    {
        IIdentifier Weather { get; }
    }
}