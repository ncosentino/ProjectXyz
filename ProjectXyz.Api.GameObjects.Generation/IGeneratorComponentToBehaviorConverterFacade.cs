using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public interface IGeneratorComponentToBehaviorConverterFacade :
        IGeneratorComponentToBehaviorConverter,
        IGeneratorComponentToBehaviorConverterRegistrar
    {
        IEnumerable<IBehavior> Convert(
            IEnumerable<IBehavior> baseBehaviors,
            IEnumerable<IGeneratorComponent> generatorComponents);
    }
}