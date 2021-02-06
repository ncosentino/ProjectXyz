using System;
using System.Linq;
using System.Threading;
using Autofac;
using ConsoleApplication1.Wip.Items.Generation.Plugins;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.ElapsedTime.Duration;
using ProjectXyz.Plugins.Features.ExpiringEnchantments;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Enchantments;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            moduleDiscoverer.AssemblyLoadFailed += (_, args) =>
            {
                Console.WriteLine($"ERROR: Could not load assembly '{args.AssemblyFilePath}'.\r\n\t{args.Exception.Message}");
            };
            var moduleDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var modules =
                moduleDiscoverer.Discover(moduleDirectory, "*.exe")
                .Concat(moduleDiscoverer
                ////.Discover(moduleDirectory, "*.Autofac.dll"))
                ////.Concat(moduleDiscoverer
                ////.Discover(moduleDirectory, "Examples.Modules.*.dll"));
                .Discover(moduleDirectory, "*.dll"));

            Console.WriteLine("Discovered modules:");
            foreach (var module in modules)
            {
                Console.WriteLine($"\t{module}");
            }

            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);

            var itemGenerator = dependencyContainer.Resolve<IItemGeneratorFacade>();
            itemGenerator.Register(new RandomRollItemGeneratorPlugin(
                new StringIdentifier("roll"),
                100));
            var generationContextFactory = dependencyContainer.Resolve<IGeneratorContextFactory>();
            var itemGenerationContext = generationContextFactory.CreateGeneratorContext(
                1,
                1,
                new GeneratorAttribute(
                    new StringIdentifier("roll"),
                    new DoubleGeneratorAttributeValue(50),
                    true));
            var generatedItems = itemGenerator
                .GenerateItems(itemGenerationContext)
                .ToArray();

            var enchantmentGenerationContext = generationContextFactory.CreateGeneratorContext(
                1,
                1,
                new GeneratorAttribute(
                    new StringIdentifier("roll"),
                    new DoubleGeneratorAttributeValue(50),
                    true));
            var enchantmentDefinitions = dependencyContainer
                .Resolve<IEnchantmentDefinitionRepository>()
                .ReadEnchantmentDefinitions(enchantmentGenerationContext)
                .ToArray();

            var gameEngine = dependencyContainer.Resolve<IAsyncGameEngine>();

            var actorFactory = dependencyContainer.Resolve<IActorFactory>();
            var actor = actorFactory.Create();

            var buffable = actor
                .Behaviors
                .GetOnly<IBuffableBehavior>();
            buffable.AddEnchantments(new IEnchantment[]
            {
                new Enchantment(
                    dependencyContainer.Resolve<IBehaviorCollectionFactory>(),
                    new IBehavior[]
                    {
                        new HasStatDefinitionIdBehavior() { StatDefinitionId = new StringIdentifier("stat1") },
                        new EnchantmentExpressionBehavior(new CalculationPriority<int>(1), "stat1 + 1"),
                        new ExpiryTriggerBehavior(new DurationTriggerBehavior(new Interval<double>(5000))),
                        dependencyContainer.Resolve<IAppliesToBaseStat>(),
                    }),
            });

            var item = generatedItems.First();

            var buffableItem = item
                .Behaviors
                .GetFirst<IBuffableBehavior>();
            buffableItem.AddEnchantments(new IEnchantment[]
            {
                new Enchantment(
                    dependencyContainer.Resolve<IBehaviorCollectionFactory>(),
                    new IBehavior[]
                    {
                        new HasStatDefinitionIdBehavior() { StatDefinitionId = new StringIdentifier("stat2") },
                        new EnchantmentExpressionBehavior(new CalculationPriority<int>(1), "stat2 + 1"),
                        new ExpiryTriggerBehavior(new DurationTriggerBehavior(new Interval<double>(5000))),
                    }),
            });

            var canEquip = actor
                .Behaviors
                .GetFirst<ICanEquipBehavior>();
            canEquip.TryEquip(
                new StringIdentifier("left hand"),
                item.Behaviors.GetFirst<ICanBeEquippedBehavior>());

            dependencyContainer
                .Resolve<IMutableGameObjectManager>()
                .MarkForAddition(actor);

            var cancellationTokenSource = new CancellationTokenSource();
            gameEngine.RunAsync(cancellationTokenSource.Token);

            Console.ReadLine();
        }
    }
}