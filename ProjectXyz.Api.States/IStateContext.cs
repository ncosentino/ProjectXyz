using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.States
{
    public interface IStateContext
    {
        double GetStateValue(IIdentifier stateId);
    }
}