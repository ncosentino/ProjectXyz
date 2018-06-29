using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked
{
    public sealed class LinkedDropTable : ILinkedDropTable
    {
        public LinkedDropTable(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> entries,
            IEnumerable<IGeneratorAttribute> supportedAttributes,
            IEnumerable<IGeneratorAttribute> providedAttributes)
        {
            DropTableId = dropTableId;
            MinimumGenerateCount = minimumGenerateCount;
            MaximumGenerateCount = maximumGenerateCount;
            Entries = entries.ToArray();
            SupportedAttributes = supportedAttributes.ToArray();
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
                Enumerable.Empty<IGeneratorAttribute>(),
                Enumerable.Empty<IGeneratorAttribute>())
        {
        }

        public IIdentifier DropTableId { get; }

        public int MinimumGenerateCount { get; } = 1;

        public int MaximumGenerateCount { get; } = 3;

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; }

        public IEnumerable<IGeneratorAttribute> ProvidedAttributes { get; }

        public IReadOnlyCollection<IWeightedEntry> Entries { get; }
    }
}