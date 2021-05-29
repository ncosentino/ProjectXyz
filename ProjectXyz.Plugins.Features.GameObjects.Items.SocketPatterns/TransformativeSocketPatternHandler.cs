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
            foreach (var definition in _transformativeSocketPatternRepository.GetAll())
            {
                if (!_attributeFilterer.IsMatch(
                    socketableInfo.Item,
                    definition.Filters))
                {
                    continue;
                }

                var newBehaviors = _componentToBehaviorConverterFacade.Convert(
                    socketableInfo.Item.Behaviors,
                    definition.GeneratorComponents);
                newItem = _gameObjectFactory.Create(newBehaviors);
                return true;
            }

            newItem = null;
            return false;
        }
    }
}
