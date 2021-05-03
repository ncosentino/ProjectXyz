using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillBehaviorsInterceptorFacade : ISkillBehaviorsInterceptorFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableSkillBehaviorsInterceptor> _interceptors;

        public SkillBehaviorsInterceptorFacade(IEnumerable<IDiscoverableSkillBehaviorsInterceptor> interceptors)
        {
            _interceptors = interceptors.ToArray();
        }

        public void Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            foreach (var interceptor in _interceptors)
            {
                interceptor.Intercept(behaviors);
            }
        }
    }
}
