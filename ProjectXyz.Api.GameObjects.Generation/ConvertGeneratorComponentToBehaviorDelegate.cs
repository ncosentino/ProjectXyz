using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public delegate IEnumerable<IBehavior> ConvertGeneratorComponentToBehaviorDelegate(IGeneratorComponent generatorComponent);
}