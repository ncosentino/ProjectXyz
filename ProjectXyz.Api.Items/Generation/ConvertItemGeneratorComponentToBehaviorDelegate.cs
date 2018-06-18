using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Api.Items.Generation
{
    public delegate IEnumerable<IBehavior> ConvertItemGeneratorComponentToBehaviorDelegate(IItemGeneratorComponent itemGeneratorComponent);
}