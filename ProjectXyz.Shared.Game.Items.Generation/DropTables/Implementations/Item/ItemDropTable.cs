using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item
{
    public sealed class ItemDropTable : IItemDropTable
    {
        public ItemDropTable(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IGeneratorAttribute> supportedAttributes,
            IEnumerable<IGeneratorAttribute> providedAttributes)
        {
            DropTableId = dropTableId;
            MinimumGenerateCount = minimumGenerateCount;
            MaximumGenerateCount = maximumGenerateCount;
            SupportedAttributes = supportedAttributes.ToArray();
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
                Enumerable.Empty<IGeneratorAttribute>(),
                Enumerable.Empty<IGeneratorAttribute>())
        {
        }

        public IIdentifier DropTableId { get; }

        public int MinimumGenerateCount { get; }

        public int MaximumGenerateCount { get; }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; }

        public IEnumerable<IGeneratorAttribute> ProvidedAttributes { get; }
    }
}