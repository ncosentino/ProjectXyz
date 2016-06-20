using ClassLibrary1.Application.Interface.Stats;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Shared.Stats
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