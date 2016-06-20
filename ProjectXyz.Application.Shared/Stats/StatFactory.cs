using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Shared.Stats
{
    public sealed class StatFactory : IStatFactory
    {
        public IStat Create(
            IIdentifier statDefinitionId,
            double value)
        {
            return new Stat(
                statDefinitionId,
                value);
        }
    }
}