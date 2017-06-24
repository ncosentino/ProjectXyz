using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public interface IStateContext
    {
        double GetStateValue(IIdentifier stateId);
    }
}