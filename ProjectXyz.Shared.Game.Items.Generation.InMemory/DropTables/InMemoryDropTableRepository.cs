using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables
{
    public sealed class InMemoryDropTableRepository : IDiscoverableDropTableRepository
    {
        private readonly Lazy<IReadOnlyDictionary<IIdentifier, IDropTable>> _lazyDropTables;

        public InMemoryDropTableRepository(IEnumerable<IDropTable> dropTables)
        {
            _lazyDropTables = new Lazy<IReadOnlyDictionary<IIdentifier, IDropTable>>(() => dropTables.ToDictionary(x => x.DropTableId, x => x));
        }

        private IReadOnlyDictionary<IIdentifier, IDropTable> DropTables => _lazyDropTables.Value;

        public IEnumerable<IDropTable> GetAllDropTables() => DropTables.Values;

        public IDropTable GetForDropTableId(IIdentifier dropTableId) => DropTables
            .First(x => Equals(x.Key, dropTableId))
            .Value;
    }
}