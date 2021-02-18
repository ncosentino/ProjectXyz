using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

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
            IGeneratorContext generatorContext)
            where T : IHasGeneratorAttributes
        {
            var requiredContextAttributes = new Dictionary<IIdentifier, IGeneratorAttribute>();
            var allContextAttributes = new Dictionary<IIdentifier, IGeneratorAttribute>();
            foreach (var contextAttribute in generatorContext.Attributes)
            {
                allContextAttributes.Add(contextAttribute.Id, contextAttribute);
                if (contextAttribute.Required)
                {
                    requiredContextAttributes.Add(contextAttribute.Id, contextAttribute);
                }
            }

            foreach (var sourceToFilter in source)
            {
                var requiredSourceAttributes = new Dictionary<IIdentifier, IGeneratorAttribute>();
                var allSourceAttributes = new Dictionary<IIdentifier, IGeneratorAttribute>();
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