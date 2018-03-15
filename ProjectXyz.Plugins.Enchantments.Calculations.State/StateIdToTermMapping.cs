using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.StateEnchantments.Api;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public sealed class StateIdToTermMapping : IStateIdToTermMapping
    {
        public StateIdToTermMapping(
            IIdentifier stateIdentifier,
            ITermMapping termMapping)
        {
            StateIdentifier = stateIdentifier;
            TermMapping = termMapping;
        }

        public IIdentifier StateIdentifier { get; }

        public ITermMapping TermMapping { get; }
    }
}
