using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Standard
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