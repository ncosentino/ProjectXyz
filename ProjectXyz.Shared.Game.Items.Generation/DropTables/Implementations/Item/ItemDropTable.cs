using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables.Standard;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item
{
    public sealed class ItemDropTable : IItemDropTable
    {
        public ItemDropTable(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes)
        {
            DropTableId = dropTableId;
            MinimumGenerateCount = minimumGenerateCount;
            MaximumGenerateCount = maximumGenerateCount;
            SupportedAttributes = supportedAttributes.ToArray();
            ProvidedAttributes = providedAttributes.ToArray();
        }

        public IIdentifier DropTableId { get; }

        public int MinimumGenerateCount { get; }

        public int MaximumGenerateCount { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IFilterAttribute> ProvidedAttributes { get; }
    }
}