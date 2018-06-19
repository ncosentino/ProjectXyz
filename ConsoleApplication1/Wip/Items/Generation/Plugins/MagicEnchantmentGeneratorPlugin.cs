using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ConsoleApplication1.Wip.Items.Generation.Plugins
{
    public sealed class MagicEnchantmentGeneratorPlugin : IEnchantmentGenerator
    {
        private readonly IBaseEnchantmentGenerator _baseEnchantmentGenerator;

        public MagicEnchantmentGeneratorPlugin(IBaseEnchantmentGenerator baseEnchantmentGenerator)
        {
            _baseEnchantmentGenerator = baseEnchantmentGenerator;
        }

        public IEnumerable<IEnchantment> GenerateEnchantments(IGeneratorContext generatorContext)
        {
            var enchantments = _baseEnchantmentGenerator.GenerateEnchantments(generatorContext);
            return enchantments;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = new IGeneratorAttribute[]
        {
            new GeneratorAttribute(new StringIdentifier("affix-type"), new StringGeneratorAttributeValue("magic")),
        };
    }
}