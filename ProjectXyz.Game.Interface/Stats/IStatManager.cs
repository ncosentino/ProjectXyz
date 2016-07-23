using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Game.Interface.Stats
{
    public interface IStatManager
    {
        double GetValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId);
    }
}