using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables
{
    public interface IDropTable : IHasFilterAttributes
    {
        IIdentifier DropTableId { get; }

        int MinimumGenerateCount { get; }

        int MaximumGenerateCount { get; }

        IEnumerable<IFilterAttribute> ProvidedAttributes { get; }
    }
}