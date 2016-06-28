using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IStatCalculationNodeWrapper
    {
        IStatCalculationNode Wrap(
            IIdentifier statDefinitionId,
            IStatCalculationNode statCalculationNode);
    }
}