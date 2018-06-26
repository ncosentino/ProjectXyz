using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IReadOnlyWeatherManager
    {
        IIdentifier WeatherId { get; }
    }
}