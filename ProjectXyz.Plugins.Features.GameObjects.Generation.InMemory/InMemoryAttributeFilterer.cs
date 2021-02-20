using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.InMemory
{
    public sealed class InMemoryAttributeFilterer : IAttributeFilterer
    {
        private readonly IAttributeValueMatcher _attributeValueMatcher;

        public InMemoryAttributeFilterer(IAttributeValueMatcher attributeValueMatcher)
        {
            _attributeValueMatcher = attributeValueMatcher;
        }

        public IEnumerable<T> Filter<T>(
            IEnumerable<T> source,
            IFilterContext filterContext)
            where T : IHasFilterAttributes
        {
            var requiredContextAttributes = new Dictionary<IIdentifier, IFilterAttribute>();
            var allContextAttributes = new Dictionary<IIdentifier, IFilterAttribute>();
            foreach (var contextAttribute in filterContext.Attributes)
            {
                allContextAttributes.Add(contextAttribute.Id, contextAttribute);
                if (contextAttribute.Required)
                {
                    requiredContextAttributes.Add(contextAttribute.Id, contextAttribute);
                }
            }

            foreach (var sourceToFilter in source)
            {
                var requiredSourceAttributes = new Dictionary<IIdentifier, IFilterAttribute>();
                var allSourceAttributes = new Dictionary<IIdentifier, IFilterAttribute>();
                foreach (var sourceAttribute in sourceToFilter.SupportedAttributes)
                {
                    allSourceAttributes.Add(sourceAttribute.Id, sourceAttribute);
                    if (sourceAttribute.Required)
                    {
                        requiredSourceAttributes.Add(sourceAttribute.Id, sourceAttribute);
                    }
                }

                var matchAttributeCache = new HashSet<IIdentifier>();

                bool metContextRequirements = true;
                foreach (var requiredContextAttribute in requiredContextAttributes
                    .Keys
                    .Where(x => !matchAttributeCache.Contains(x)))
                {
                    if (!allSourceAttributes.ContainsKey(requiredContextAttribute))
                    {
                        metContextRequirements = false;
                        break;
                    }

                    var isAttrMatch = _attributeValueMatcher.Match(
                        allSourceAttributes[requiredContextAttribute].Value,
                        allContextAttributes[requiredContextAttribute].Value);
                    if (!isAttrMatch)
                    {
                        metContextRequirements = false;
                        break;
                    }

                    matchAttributeCache.Add(requiredContextAttribute);
                }

                if (!metContextRequirements)
                {
                    continue;
                }

                bool metSourceRequirements = true;
                foreach (var requiredSourceAttribute in requiredSourceAttributes
                    .Keys
                    .Where(x => !matchAttributeCache.Contains(x)))
                {
                    if (!allContextAttributes.ContainsKey(requiredSourceAttribute))
                    {
                        metSourceRequirements = false;
                        break;
                    }

                    var isAttrMatch = _attributeValueMatcher.Match(
                        allSourceAttributes[requiredSourceAttribute].Value,
                        allContextAttributes[requiredSourceAttribute].Value);
                    if (!isAttrMatch)
                    {
                        metSourceRequirements = false;
                        break;
                    }

                    matchAttributeCache.Add(requiredSourceAttribute);
                }

                if (!metSourceRequirements)
                {
                    continue;
                }

                yield return sourceToFilter;
            }
        }
    }
}