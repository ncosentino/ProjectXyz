using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables
{
    public interface IDropTable : IHasFilterAttributes
    {
        IIdentifier DropTableId { get; }

        int MinimumGenerateCount { get; }

        int MaximumGenerateCount { get; }

        IEnumerable<IFilterAttribute> ProvidedAttributes { get; }
    }
}