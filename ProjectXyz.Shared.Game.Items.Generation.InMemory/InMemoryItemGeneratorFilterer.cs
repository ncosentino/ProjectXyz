using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;
using ProjectXyz.Framework.Extensions.Collections;

namespace ProjectXyz.Shared.Game.Items.Generation.InMemory
{
    public sealed class InMemoryItemGeneratorFilterer : IItemGeneratorFilterer
    {
        private readonly IAttributeValueMatcher _attributeValueMatcher;

        public InMemoryItemGeneratorFilterer(IAttributeValueMatcher attributeValueMatcher)
        {
            _attributeValueMatcher = attributeValueMatcher;
        }

        public IEnumerable<IItemGenerator> Filter(
            IEnumerable<IItemGenerator> itemGenerators, 
            IItemGeneratorContext itemGeneratorContext)
        {
            var attributeToContextMapping = itemGeneratorContext
                .Attributes
                .GroupBy(x => x.Id)
                .ToDictionary(
                    x => x.Key,
                    x => x
                        .Select(key => key.Value)
                        .ToReadOnlyCollection());
            var matchingGenerators = itemGenerators
                .Where(generator =>
                {
                    var matchingAttributes = generator
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
            return matchingGenerators;
        }
    }
}