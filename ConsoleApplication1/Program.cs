using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            Console.ReadLine();
        }
    }

    public sealed class DummyEntity : IEntity
    {
        public DummyEntity(IEnumerable<IComponent> components)
        {
            Components = new ComponentCollection(components);
        }

        public IComponentCollection Components { get; }
    }

    public sealed class HasEnchantmentsWrapperComponent : IHasEnchantmentsComponent
    {
        public HasEnchantmentsWrapperComponent(IReadOnlyCollection<IEnchantment> enchantments)
        {
            Enchantments = enchantments;
        }

        public IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }

    public interface IHasEnchantmentsComponent : IComponent
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }

    public interface IEnchantmentApplyTypeComponent : IComponent
    {
        IIdentifier ApplyTypeId { get; }
    }

    public sealed class EnchantmentApplyTypeComponent : IEnchantmentApplyTypeComponent
    {
        public EnchantmentApplyTypeComponent(IIdentifier applyTypeId)
        {
            ApplyTypeId = applyTypeId;
        }

        public IIdentifier ApplyTypeId { get; }
    }
}