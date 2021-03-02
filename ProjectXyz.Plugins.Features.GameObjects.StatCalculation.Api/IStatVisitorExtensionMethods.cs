using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public static class IStatVisitorExtensionMethods
    {
        public static double GetStatValue(
            this IStatVisitor statVisitor,
            IHasBehaviors gameObject,
            IIdentifier statId)
        {
            var value = statVisitor.GetStatValue(
                gameObject,
                statId,
                EmptyCalculationContext.Instance);
            return value;
        }

        private sealed class EmptyCalculationContext : IStatCalculationContext
        {
            private static Lazy<IStatCalculationContext> _lazyEmpty = new Lazy<IStatCalculationContext>(() =>
            {
                return new EmptyCalculationContext();
            });

            public static IStatCalculationContext Instance => _lazyEmpty.Value;

            private EmptyCalculationContext()
            {
                Enchantments = new IEnchantment[0];
                Components = new ComponentCollection();
            }

            public IReadOnlyCollection<IEnchantment> Enchantments { get; }

            public IComponentCollection Components { get; }
        }

        private sealed class ComponentCollection : IComponentCollection
        {
            public int Count { get; } = 0;

            public IEnumerable<TComponent> Get<TComponent>()
                where TComponent : IComponent => Enumerable.Empty<TComponent>();

            public IEnumerator<IComponent> GetEnumerator() => Enumerable.Empty<IComponent>().GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
