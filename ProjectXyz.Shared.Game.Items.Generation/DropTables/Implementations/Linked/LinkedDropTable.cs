using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Linked;

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
            SupportedAttributes = supportedAttributes.ToArray();
            ProvidedAttributes = providedAttributes.ToArray();
        }

        public IIdentifier DropTableId { get; }

        public int MinimumGenerateCount { get; }

        public int MaximumGenerateCount { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IFilterAttribute> ProvidedAttributes { get; }

        public IReadOnlyCollection<IWeightedEntry> Entries { get; }
    }
}