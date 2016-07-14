using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentCalculatorContext : IEnchantmentCalculatorContext
    {
        #region Constants
        private static readonly Lazy<IEnchantmentCalculatorContext> NONE = new Lazy<IEnchantmentCalculatorContext>(() => new EnchantmentCalculatorContext(
            Core.Enchantments.StateContextProvider.Empty,
            new Interval<double>(0),
            new IEnchantment[0]));
        #endregion

        #region Constructors
        public EnchantmentCalculatorContext(
            IStateContextProvider stateContextProvider,
            IInterval elapsed,
            IReadOnlyCollection<IEnchantment> enchantments)
        {
            StateContextProvider = stateContextProvider;
            Elapsed = elapsed;
            Enchantments = enchantments;
        }
        #endregion

        #region Properties
        public static IEnchantmentCalculatorContext None = NONE.Value;

        public IReadOnlyCollection<IEnchantment> Enchantments { get; }

        public IInterval Elapsed { get; }

        public IStateContextProvider StateContextProvider { get; }
        #endregion

        #region Methods
        public IEnchantmentCalculatorContext WithEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            return new EnchantmentCalculatorContext(
                StateContextProvider,
                Elapsed,
                enchantments.ToArray());
        }

        public IEnchantmentCalculatorContext WithStateContextProvider(IStateContextProvider stateContextProvider)
        {
            return new EnchantmentCalculatorContext(
                stateContextProvider,
                Elapsed,
                Enchantments.ToArray());
        }

        public IEnchantmentCalculatorContext WithElapsed(IInterval elapsed)
        {
            return new EnchantmentCalculatorContext(
                StateContextProvider,
                elapsed,
                Enchantments.ToArray());
        }
        #endregion
    }
}