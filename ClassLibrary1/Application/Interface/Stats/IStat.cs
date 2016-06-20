using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Interface.Stats
{
    public interface IStat
    {
        double Value { get; }

        IIdentifier StatDefinitionId { get; }
    }
}