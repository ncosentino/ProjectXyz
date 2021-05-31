using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns
{
    public sealed class TransformativeSocketPatternHandler : IDiscoverableSocketPatternHandler
    {
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IGeneratorComponentToBehaviorConverterFacade _componentToBehaviorConverterFacade;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly ITransformativeSocketPatternRepositoryFacade _transformativeSocketPatternRepository;

        public TransformativeSocketPatternHandler(
            IAttributeFilterer attributeFilterer,
            IGeneratorComponentToBehaviorConverterFacade componentToBehaviorConverterFacade,
            IGameObjectFactory gameObjectFactory,
            ITransformativeSocketPatternRepositoryFacade transformativeSocketPatternRepository) 
        {
            _attributeFilterer = attributeFilterer;
            _componentToBehaviorConverterFacade = componentToBehaviorConverterFacade;
            _gameObjectFactory = gameObjectFactory;
            _transformativeSocketPatternRepository = transformativeSocketPatternRepository;
        }

        public bool TryHandle(
            ISocketableInfo socketableInfo,
            out IGameObject newItem)
        {
            IGameObject resultItem = null;
            Parallel.ForEach(_transformativeSocketPatternRepository.GetAll(), (definition, definitionsLoopState, _) =>
            {
                if (!_attributeFilterer.IsMatch(
                    socketableInfo.Item,
                    definition.Filters))
                {
                    return;
                }

                if (!definitionsLoopState.IsStopped)
                {
                    definitionsLoopState.Stop();
                    var newBehaviors = _componentToBehaviorConverterFacade.Convert(
                        socketableInfo.Item.Behaviors,
                        definition.GeneratorComponents);
                    resultItem = _gameObjectFactory.Create(newBehaviors);
                }
            });

            newItem = resultItem;
            return newItem != null;
        }
    }
}
