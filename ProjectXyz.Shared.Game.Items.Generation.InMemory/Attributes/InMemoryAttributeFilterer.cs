using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes
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
            IItemGeneratorContext itemGeneratorContext)
            where T : IHasItemGeneratorAttributes
        {
            var attributeToContextMapping = itemGeneratorContext
                .Attributes
                .GroupBy(x => x.Id)
                .ToDictionary(
                    x => x.Key,
                    x => x
                        .Select(key => key.Value)
                        .ToReadOnlyCollection());
            var matching = source
                .Where(s=>
                {
                    var requiredAttributes = s.
                        SupportedAttributes
                        .ToDictionary(
                            x => x.Id,
                            x => x.Value);
                    var missingRequiredAttributes = requiredAttributes
                        .Keys
                        .Any(x => !attributeToContextMapping.ContainsKey(x));
                    if (missingRequiredAttributes)
                    {
                        return false;
                    }

                    var matchingAttributes = s
                        .SupportedAttributes
                        .Where(attr => attributeToContextMapping.ContainsKey(attr.Id))
                        .ToArray();
                    if (!matchingAttributes.Any())
                    {
                        return true;
                    }

                    var isGeneratorMatch = matchingAttributes
                        .Any(attr => attributeToContextMapping[attr.Id]
                            .Any(contextAttrVal =>
                            {
                                var isAttrMatch = _attributeValueMatcher.Match(
                                    attr.Value,
                                    contextAttrVal);
                                return isAttrMatch;
                            }));
                    return isGeneratorMatch;
                });
            return matching;
        }
    }
}