using System.Collections.Generic;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentCalculatorContext : IEntity
    {
        IReadOnlyCollection<IGameObject> Enchantments { get; }

        double ElapsedTurns { get; }

        IEnchantmentCalculatorContext WithEnchantments(IEnumerable<IGameObject> enchantments);

        IEnchantmentCalculatorContext WithComponent(IComponent component);

        IEnchantmentCalculatorContext WithElapsedTurns(double elapsedTurns);
    }

    public static class IEnchantmentCalculatorContextExtensions
    {
        public static IEnchantmentCalculatorContext WithEnchantments(
            this IEnchantmentCalculatorContext enchantmentCalculatorContext,
            params IGameObject[] enchantments)
        {
            return enchantmentCalculatorContext.WithEnchantments(enchantments);
        }
    }
}