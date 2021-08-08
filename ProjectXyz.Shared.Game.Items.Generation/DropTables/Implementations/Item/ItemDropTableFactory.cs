using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Standard;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item
{
    public sealed class ItemDropTableFactory : IItemDropTableFactory
    {
        private readonly IDropTableIdentifiers _dropTableIdentifiers;

        public ItemDropTableFactory(IDropTableIdentifiers dropTableIdentifiers)
        {
            _dropTableIdentifiers = dropTableIdentifiers;
        }

        public IItemDropTable Create(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IFilterAttribute> providedAttributes)
        {
            var dropTable = new ItemDropTable(
                dropTableId,
                minimumGenerateCount,
                maximumGenerateCount,
                new FilterAttribute(
                    _dropTableIdentifiers.FilterContextDropTableIdentifier,
                    new IdentifierFilterAttributeValue(dropTableId),
                    false)
                    .Yield()
                    .Concat(supportedAttributes),
                providedAttributes);
            return dropTable;
        }

        public IItemDropTable Create(
            IIdentifier dropTableId,
            int minimumGenerateCount,
            int maximumGenerateCount)
        {
            var dropTable = Create(
                dropTableId,
                minimumGenerateCount,
                maximumGenerateCount,
                Enumerable.Empty<IFilterAttribute>(),
                Enumerable.Empty<IFilterAttribute>());
            return dropTable;
        }
    }
}