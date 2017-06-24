using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.States
{
    public interface IStateIdToTermMapping
    {
        IIdentifier StateIdentifier { get; }

        ITermMapping TermMapping { get; }
    }
}