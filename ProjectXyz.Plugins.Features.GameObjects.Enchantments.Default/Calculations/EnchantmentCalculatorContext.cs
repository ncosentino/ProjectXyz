using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations
{
    public sealed class EnchantmentCalculatorContext : IEnchantmentCalculatorContext
    {
        private static readonly Lazy<IEnchantmentCalculatorContext> NONE = new Lazy<IEnchantmentCalculatorContext>(() => new EnchantmentCalculatorContext(
            ComponentCollection.Empty,
           0d,
            new IGameObject[0]));
        
        public EnchantmentCalculatorContext(
            IEnumerable<IComponent> components,
            double elapsedTurns,
            IReadOnlyCollection<IGameObject> enchantments)
        {
            Components = new ComponentCollection(components);
            ElapsedTurns = elapsedTurns;
            Enchantments = enchantments;
        }

        public static IEnchantmentCalculatorContext None = NONE.Value;

        public IComponentCollection Components { get; }

        public IReadOnlyCollection<IGameObject> Enchantments { get; }

        public double ElapsedTurns { get; }

        public IEnchantmentCalculatorContext WithEnchantments(IEnumerable<IGameObject> enchantments)
        {
            return new EnchantmentCalculatorContext(
                Components,
                ElapsedTurns,
                enchantments.ToArray());
        }

        public IEnchantmentCalculatorContext WithComponent(IComponent component)
        {
            return new EnchantmentCalculatorContext(
                new ComponentCollection(Components.AppendSingle(component)),
                ElapsedTurns,
                Enchantments.ToArray());
        }

        public IEnchantmentCalculatorContext WithElapsedTurns(double elapsedTurns)
        {
            return new EnchantmentCalculatorContext(
                Components,
                elapsedTurns,
                Enchantments.ToArray());
        }
    }
}