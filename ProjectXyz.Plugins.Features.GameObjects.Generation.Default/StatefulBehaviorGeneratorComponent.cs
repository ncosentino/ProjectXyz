using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.Default
{
    public sealed class StatefulBehaviorGeneratorComponent : IBehaviorGeneratorComponent
    {
        private readonly Func<IReadOnlyCollection<IBehavior>> _createBehaviorsCallback;

        public StatefulBehaviorGeneratorComponent(
            Func<IReadOnlyCollection<IBehavior>> createBehaviorsCallback)
        {
            _createBehaviorsCallback = createBehaviorsCallback;
        }

        /// <inheritdoc>
        /// <remarks>
        /// We use a callback here because any stateful behaviors will need to 
        /// be recreated to avoid being shared.
        /// </remarks>
        public IReadOnlyCollection<IBehavior> Behaviors => _createBehaviorsCallback.Invoke();
    }
}