using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations
{
    public sealed class EnchantmentCalculatorContext : IEnchantmentCalculatorContext
    {
        #region Constants
        private static readonly Lazy<IEnchantmentCalculatorContext> NONE = new Lazy<IEnchantmentCalculatorContext>(() => new EnchantmentCalculatorContext(
            ComponentCollection.Empty,
            new Interval<double>(0),
            new IEnchantment[0]));
        #endregion

        #region Constructors
        public EnchantmentCalculatorContext(
            IEnumerable<IComponent> components,
            IInterval elapsed,
            IReadOnlyCollection<IEnchantment> enchantments)
        {
            Components = new ComponentCollection(components);
            Elapsed = elapsed;
            Enchantments = enchantments;
        }
        #endregion

        #region Properties
        public static IEnchantmentCalculatorContext None = NONE.Value;

        public IComponentCollection Components { get; }

        public IReadOnlyCollection<IEnchantment> Enchantments { get; }

        public IInterval Elapsed { get; }
        #endregion

        #region Methods
        public IEnchantmentCalculatorContext WithEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            return new EnchantmentCalculatorContext(
                Components,
                Elapsed,
                enchantments.ToArray());
        }

        public IEnchantmentCalculatorContext WithComponent(IComponent component)
        {
            return new EnchantmentCalculatorContext(
                new ComponentCollection(Components.AppendSingle(component)),
                Elapsed,
                Enchantments.ToArray());
        }

        public IEnchantmentCalculatorContext WithElapsed(IInterval elapsed)
        {
            return new EnchantmentCalculatorContext(
                Components,
                elapsed,
                Enchantments.ToArray());
        }
        #endregion
    }
}