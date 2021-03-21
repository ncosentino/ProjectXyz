using System.Collections.Generic;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentCalculatorContext : IEntity
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }

        double ElapsedTurns { get; }

        IEnchantmentCalculatorContext WithEnchantments(IEnumerable<IEnchantment> enchantments);

        IEnchantmentCalculatorContext WithComponent(IComponent component);

        IEnchantmentCalculatorContext WithElapsedTurns(double elapsedTurns);
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