using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var targetApplyType = new StringIdentifier("Equip Apply Type ID");
            var thing1 = new DummyEntity(new ComponentCollection(new HasEnchantmentsWrapperComponent(new[]
            {
                new Enchantment(new StringIdentifier("Stat ID"), new[] { new EnchantmentApplyTypeComponent(targetApplyType), })
            })));
            var thing2 = new DummyEntity(new ComponentCollection(new HasEnchantmentsWrapperComponent(new[]
            {
                new Enchantment(new StringIdentifier("Stat ID"), new[] { new EnchantmentApplyTypeComponent(new StringIdentifier(Guid.NewGuid().ToString())), })
            })));
            
            var equipEnchantments = thing1
                .Components
                .Get<IHasEnchantmentsComponent>()
                .WithApplyType(targetApplyType)
                .ToArray();
            Console.WriteLine("Thing1 Equip Enchantments: " + equipEnchantments.Length);

            equipEnchantments = thing2
                .Components
                .Get<IHasEnchantmentsComponent>()
                .WithApplyType(targetApplyType)
                .ToArray();
            Console.WriteLine("Thing2 Equip Enchantments: " + equipEnchantments.Length);
            Console.ReadLine();
        }
    }

    

    public static class lol
    {
        public static IEnumerable<IEnchantment> WithApplyType(
            this IEnumerable<IHasEnchantmentsComponent> hasEnchantmentsComponents,
            IIdentifier applyTypeId)
        {
            return hasEnchantmentsComponents
                .SingleOrDefault(x => x.Enchantments)
                .Where(x => x.Components.Get<IEnchantmentApplyTypeComponent>().Any(c => c.ApplyTypeId.Equals(applyTypeId)));
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