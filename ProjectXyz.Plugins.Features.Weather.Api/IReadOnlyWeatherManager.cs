using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IReadOnlyWeatherManager
    {
        IIdentifier WeatherId { get; }
    }
}