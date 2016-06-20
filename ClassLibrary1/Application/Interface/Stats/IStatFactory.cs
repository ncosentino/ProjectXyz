using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Interface.Stats
{
    public interface IStatFactory
    {
        IStat Create(
            IIdentifier statDefinitionId,
            double value);
    }
}