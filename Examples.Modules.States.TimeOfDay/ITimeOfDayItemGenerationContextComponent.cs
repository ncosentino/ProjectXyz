using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Items;

namespace ProjectXyz.Plugins.Features.TimeOfDay
{
    public interface ITimeOfDayItemGenerationContextComponent : IItemGenerationContextComponent
    {
        IIdentifier TimeOfDay { get; }
    }
}