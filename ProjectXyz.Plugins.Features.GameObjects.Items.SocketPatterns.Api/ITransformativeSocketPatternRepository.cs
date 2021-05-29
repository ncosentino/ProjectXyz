using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api
{
    public interface ITransformativeSocketPatternRepository
    {
        IEnumerable<ITransformativeSocketPatternDefinition> GetAll();
    }
}
