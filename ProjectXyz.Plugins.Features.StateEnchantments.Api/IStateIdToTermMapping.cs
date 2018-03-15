using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.StateEnchantments.Api
{
    public interface IStateIdToTermMapping
    {
        IIdentifier StateIdentifier { get; }

        ITermMapping TermMapping { get; }
    }
}