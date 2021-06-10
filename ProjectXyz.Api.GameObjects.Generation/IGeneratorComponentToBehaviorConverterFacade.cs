using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public interface IGeneratorComponentToBehaviorConverterFacade :
        IGeneratorComponentToBehaviorConverter,
        IGeneratorComponentToBehaviorConverterRegistrar
    {
        IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IEnumerable<IGeneratorComponent> generatorComponents);
    }
}