using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default
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
