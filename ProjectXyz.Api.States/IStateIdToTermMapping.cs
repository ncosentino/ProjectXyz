using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.States
{
    public interface IStateIdToTermMapping
    {
        IIdentifier StateIdentifier { get; }

        ITermMapping TermMapping { get; }
    }
}