using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States
{
    public interface IStateIdToTermMapping
    {
        IIdentifier StateIdentifier { get; }

        ITermMapping TermMapping { get; }
    }
}