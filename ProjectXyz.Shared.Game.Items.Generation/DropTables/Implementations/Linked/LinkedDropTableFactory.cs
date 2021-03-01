using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables.Linked;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked
{
    public sealed class LinkedDropTableFactory : ILinkedDropTableFactory
    {
        private readonly IDropTableIdentifiers _dropTableIdentifiers;

        public LinkedDropTableFactory(IDropTableIdentifiers dropTableIdentifiers)
        {
            _dropTableIdentifiers = dropTableIdentifiers;
        }

        public ILinkedDropTable Create(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> weightedEntries,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes)
        {
            var dropTable = new LinkedDropTable(
                dropTableId,
                minimumGenerateCount,
                maximumGenerateCount,
                weightedEntries,
                new FilterAttribute(
                    _dropTableIdentifiers.FilterContextDropTableIdentifier,
                    new IdentifierFilterAttributeValue(dropTableId),
                    false)
                    .Yield()
                    .Concat(supportedAttributes),
                providedAttributes);
            return dropTable;
        }

        public ILinkedDropTable Create(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IWeightedEntry> weightedEntries)
        {
            var dropTable = Create(
                dropTableId,
                minimumGenerateCount,
                maximumGenerateCount,
                weightedEntries,
                Enumerable.Empty<IFilterAttribute>(),
                Enumerable.Empty<IFilterAttribute>());
            return dropTable;
        }
    }
}