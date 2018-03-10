using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;

namespace ProjectXyz.Application.States.Core
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
