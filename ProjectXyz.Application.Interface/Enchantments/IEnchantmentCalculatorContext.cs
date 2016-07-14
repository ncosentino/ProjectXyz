using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentCalculatorContext
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }

        IInterval Elapsed { get; }

        IStateContextProvider StateContextProvider { get; }

        IEnchantmentCalculatorContext WithEnchantments(IEnumerable<IEnchantment> enchantments);

        IEnchantmentCalculatorContext WithStateContextProvider(IStateContextProvider stateContextProvider);

        IEnchantmentCalculatorContext WithElapsed(IInterval elapsed);
    }

    public static class IEnchantmentCalculatorContextExtensions
    {
        public static IEnchantmentCalculatorContext WithEnchantments(
            this IEnchantmentCalculatorContext enchantmentCalculatorContext,
            params IEnchantment[] enchantments)
        {
            return enchantmentCalculatorContext.WithEnchantments(enchantments);
        }
    }
}