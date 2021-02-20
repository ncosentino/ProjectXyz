using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked
{
    public sealed class LinkedDropTable : ILinkedDropTable
    {
        public LinkedDropTable(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> entries,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes)
        {
            DropTableId = dropTableId;
            MinimumGenerateCount = minimumGenerateCount;
            MaximumGenerateCount = maximumGenerateCount;
            Entries = entries.ToArray();
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

        public LinkedDropTable(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> entries)
            : this(
                dropTableId,
                minimumGenerateCount,
                maximumGenerateCount,
                entries,
                Enumerable.Empty<IFilterAttribute>(),
                Enumerable.Empty<IFilterAttribute>())
        {
        }

        public IIdentifier DropTableId { get; }

        public int MinimumGenerateCount { get; } = 1;

        public int MaximumGenerateCount { get; } = 3;

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IFilterAttribute> ProvidedAttributes { get; }

        public IReadOnlyCollection<IWeightedEntry> Entries { get; }
    }
}