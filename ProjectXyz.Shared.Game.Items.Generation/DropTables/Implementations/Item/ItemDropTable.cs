using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Shared.Framework;

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
            SupportedAttributes = 
                new FilterAttribute(
                    new StringIdentifier("drop-table"),
                    new IdentifierFilterAttributeValue(dropTableId),
                    false)
                .Yield()
                .Concat(supportedAttributes)
                .ToArray();
            ProvidedAttributes = providedAttributes.ToArray();
        }

        public ItemDropTable(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount)
            : this(
                dropTableId,
                minimumGenerateCount,
                maximumGenerateCount,
                Enumerable.Empty<IFilterAttribute>(),
                Enumerable.Empty<IFilterAttribute>())
        {
        }

        public IIdentifier DropTableId { get; }

        public int MinimumGenerateCount { get; }

        public int MaximumGenerateCount { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IFilterAttribute> ProvidedAttributes { get; }
    }
}