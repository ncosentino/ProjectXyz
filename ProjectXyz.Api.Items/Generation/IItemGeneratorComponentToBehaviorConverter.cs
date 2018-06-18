using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Api.Items.Generation
{
    public interface IItemGeneratorComponentToBehaviorConverter
    {
        IEnumerable<IBehavior> Convert(IItemGeneratorComponent itemGeneratorComponent);
    }
}