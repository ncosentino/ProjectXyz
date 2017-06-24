using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.States
{
    public interface IStateContext
    {
        double GetStateValue(IIdentifier stateId);
    }
}