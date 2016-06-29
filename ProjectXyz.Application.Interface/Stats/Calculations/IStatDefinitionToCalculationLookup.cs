using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IStatDefinitionToCalculationLookup
    {
        IStatCalculationNode GetCalculationNode(IIdentifier statDefinitionId);
    }
}