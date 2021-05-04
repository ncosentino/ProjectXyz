using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class BehaviorConverterFacade : IBehaviorConverterFacade
    {
        private readonly IReadOnlyCollection<IBehaviorConverter> _discoverableBehaviorConverters;

        public BehaviorConverterFacade(
            IEnumerable<IBehaviorConverter> discoverableBehaviorConverters)
        {
            _discoverableBehaviorConverters = discoverableBehaviorConverters.ToArray();
        }

        public bool CanConvert(IGeneratorComponent component)
        {
            return _discoverableBehaviorConverters.Any(x => x.CanConvert(component));
        }

        public IBehavior ConvertToBehavior(IGeneratorComponent component)
        {
            var converter = _discoverableBehaviorConverters
                .FirstOrDefault(x => x.CanConvert(component));

            return converter == null
                ? null
                : converter.ConvertToBehavior(component);
        }
    }
}
