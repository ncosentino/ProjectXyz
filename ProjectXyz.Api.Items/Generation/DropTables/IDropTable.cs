using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables
{
    public interface IDropTable : IHasGeneratorAttributes
    {
        IIdentifier DropTableId { get; }

        int MinimumGenerateCount { get; }

        int MaximumGenerateCount { get; }

        IEnumerable<IGeneratorAttribute> ProvidedAttributes { get; }
    }
}