using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables
{
    public sealed class DropTableRepositoryFacade : IDropTableRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDropTableRepository> _dropTableRepositories;

        public DropTableRepositoryFacade(IEnumerable<IDiscoverableDropTableRepository> dropTableRepositories)
        {
            _dropTableRepositories = dropTableRepositories.ToArray();
        }

        public IEnumerable<IDropTable> GetAllDropTables() =>
            _dropTableRepositories
                .SelectMany(x => x.GetAllDropTables());

        public IDropTable GetForDropTableId(IIdentifier dropTableId) =>
            _dropTableRepositories
                .Select(x => x.GetForDropTableId(dropTableId))
                .FirstOrDefault(x => x != null);
    }
}