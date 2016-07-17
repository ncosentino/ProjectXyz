using System;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Enchantments.Expiration;
using ProjectXyz.Application.Core.Triggering;
using ProjectXyz.Application.Core.Triggering.Triggers.Duration;
using ProjectXyz.Application.Core.Triggering.Triggers.Elapsed;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var elapsedTimeTriggerSourceMechanic = new ElapsedTimeTriggerSourceMechanic();

            var registrar = new TriggerMechanicRegistrar(elapsedTimeTriggerSourceMechanic.AsArray());

            var durationTriggerMechanicFactory = new DurationTriggerMechanicFactory();
            var expiryTriggerMechanicFactory = new ExpiryTriggerMechanicFactory(durationTriggerMechanicFactory);
            var activeEnchantmentManager = new ActiveEnchantmentManager(
                expiryTriggerMechanicFactory,
                registrar);

            Console.WriteLine("Press enter to add.");

            var triggerComponent = new DurationTriggerComponent(new Interval<double>(5));
            var expiryComponent = new ExpiryTriggerComponent(triggerComponent);

            activeEnchantmentManager.Add(new Enchantment(
                new StringIdentifier("xxx"), 
                new[]
                { 
                    expiryComponent,
                }));
            Console.WriteLine("Count: " + activeEnchantmentManager.Enchantments.Count);

            Console.WriteLine("Press enter to trigger.");
            Console.ReadLine();
            elapsedTimeTriggerSourceMechanic.Update(new Interval<double>(5));

            Console.WriteLine("Count: " + activeEnchantmentManager.Enchantments.Count);
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}