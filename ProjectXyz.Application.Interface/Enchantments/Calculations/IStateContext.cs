using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments.Calculations
{
    public interface IStateContext
    {
        double GetStateValue(IIdentifier stateId);
    }
}