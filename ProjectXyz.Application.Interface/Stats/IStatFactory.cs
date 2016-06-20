using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats
{
    public interface IStatFactory
    {
        IStat Create(
            IIdentifier statDefinitionId,
            double value);
    }
}