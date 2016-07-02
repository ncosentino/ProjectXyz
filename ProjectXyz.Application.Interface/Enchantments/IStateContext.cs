using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IStateContext
    {
        double GetStateValue(IIdentifier stateId);
    }
}