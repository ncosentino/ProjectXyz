using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables.Standard
{
    public interface IItemDropTableFactory
    {
        IItemDropTable Create(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount);
        
        IItemDropTable Create(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes);
    }
}