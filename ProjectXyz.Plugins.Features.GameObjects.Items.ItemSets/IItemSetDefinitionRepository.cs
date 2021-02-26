using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public interface IItemSetDefinitionRepository
    {
        IItemSetDefinition GetItemSetDefinitionById(IIdentifier itemSetDefinitionId);
    }
}
