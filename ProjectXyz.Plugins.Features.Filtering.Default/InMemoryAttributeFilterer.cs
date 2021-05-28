using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default
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
            IReadOnlyCollection<IFilterAttributeValue> filterAttributes)
        {
            foreach (var sourceToFilter in source)
            {
                var metContextRequirements = true;
                foreach (var filterAttribute in filterAttributes)
                {
                    var isAttrMatch = _attributeValueMatcher.Match(
                        filterAttribute,
                        sourceToFilter);
                    if (!isAttrMatch)
                    {
                        metContextRequirements = false;
                        break;
                    }
                }

                if (!metContextRequirements)
                {
                    continue;
                }

                yield return sourceToFilter;
            }
        }

        public IEnumerable<T> Filter<T>(
            IEnumerable<T> source,
            IEnumerable<IFilterAttribute> filterAttributes)
            where T : IHasFilterAttributes
        {
            var requiredFilterAttributes = new Dictionary<IIdentifier, IFilterAttribute>();
            var allFilterAttributes = new Dictionary<IIdentifier, IFilterAttribute>();
            foreach (var filterAttribute in filterAttributes)
            {
                allFilterAttributes.Add(filterAttribute.Id, filterAttribute);
                if (filterAttribute.Required)
                {
                    requiredFilterAttributes.Add(filterAttribute.Id, filterAttribute);
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

                var metFilterRequirements = true;
                foreach (var requiredFilterAttribute in requiredFilterAttributes
                    .Keys
                    .Where(x => !matchAttributeCache.Contains(x)))
                {
                    if (!allSourceAttributes.ContainsKey(requiredFilterAttribute))
                    {
                        metFilterRequirements = false;
                        break;
                    }

                    var isAttrMatch = _attributeValueMatcher.Match(
                        allSourceAttributes[requiredFilterAttribute].Value,
                        allFilterAttributes[requiredFilterAttribute].Value);
                    if (!isAttrMatch)
                    {
                        metFilterRequirements = false;
                        break;
                    }

                    matchAttributeCache.Add(requiredFilterAttribute);
                }

                if (!metFilterRequirements)
                {
                    continue;
                }

                yield return sourceToFilter;
            }
        }

        public IEnumerable<T> BidirectionalFilter<T>(
            IEnumerable<T> source,
            IEnumerable<IFilterAttribute> filterAttributes)
            where T : IHasFilterAttributes
        {
            var requiredFilterAttributes = new Dictionary<IIdentifier, IFilterAttribute>();
            var allFilterAttributes = new Dictionary<IIdentifier, IFilterAttribute>();
            foreach (var filterAttribute in filterAttributes)
            {
                allFilterAttributes.Add(filterAttribute.Id, filterAttribute);
                if (filterAttribute.Required)
                {
                    requiredFilterAttributes.Add(filterAttribute.Id, filterAttribute);
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

                var metFilterRequirements = true;
                foreach (var requiredFilterAttribute in requiredFilterAttributes
                    .Keys
                    .Where(x => !matchAttributeCache.Contains(x)))
                {
                    if (!allSourceAttributes.ContainsKey(requiredFilterAttribute))
                    {
                        metFilterRequirements = false;
                        break;
                    }

                    var isAttrMatch = _attributeValueMatcher.Match(
                        allSourceAttributes[requiredFilterAttribute].Value,
                        allFilterAttributes[requiredFilterAttribute].Value);
                    if (!isAttrMatch)
                    {
                        metFilterRequirements = false;
                        break;
                    }

                    matchAttributeCache.Add(requiredFilterAttribute);
                }

                if (!metFilterRequirements)
                {
                    continue;
                }

                var metSourceRequirements = true;
                foreach (var requiredSourceAttribute in requiredSourceAttributes
                    .Keys
                    .Where(x => !matchAttributeCache.Contains(x)))
                {
                    if (!allFilterAttributes.ContainsKey(requiredSourceAttribute))
                    {
                        metSourceRequirements = false;
                        break;
                    }

                    var isAttrMatch = _attributeValueMatcher.Match(
                        allSourceAttributes[requiredSourceAttribute].Value,
                        allFilterAttributes[requiredSourceAttribute].Value);
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