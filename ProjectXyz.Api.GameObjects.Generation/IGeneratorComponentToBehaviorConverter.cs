using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public interface IGeneratorComponentToBehaviorConverter
    {
        IEnumerable<IBehavior> Convert(IGeneratorComponent generatorComponent);
    }
}