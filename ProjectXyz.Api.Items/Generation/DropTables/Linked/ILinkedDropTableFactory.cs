using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables.Linked
{
    public interface ILinkedDropTableFactory
    {
        ILinkedDropTable Create(
            IIdentifier dropTableId, 
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> weightedEntries);
        
        ILinkedDropTable Create(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> weightedEntries,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes);
    }
}