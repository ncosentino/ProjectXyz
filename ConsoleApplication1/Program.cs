using System;
using System.Linq;
using Autofac;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            var moduleDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var modules =
                moduleDiscoverer.Discover(moduleDirectory, "*.exe")
                .Concat(moduleDiscoverer
                .Discover(moduleDirectory, "*.Autofac.dll"))
                .Concat(moduleDiscoverer
                .Discover(moduleDirectory, "Examples.Modules.*.dll"));
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);
            
            ////var itemGenerationContextFactory = dependencyContainer.Resolve<IItemGenerationContextFactory>();
            ////var itemGenerationContext = itemGenerationContextFactory.Merge(
            ////    itemGenerationContextFactory.Create(),
            ////    new []
            ////    {
            ////        new ItemCountContextComponent(1, 1), 
            ////    });
            ////var generatedItems = dependencyContainer
            ////    .Resolve<IItemGenerator>()
            ////    .GenerateItems(itemGenerationContext)
            ////    .ToArray();

            ////var gameEngine = dependencyContainer.Resolve<IAsyncGameEngine>();

            ////var actorFactory = dependencyContainer.Resolve<IActorFactory>();
            ////var actor = actorFactory.Create();

            ////var buffable = actor
            ////    .Behaviors
            ////    .GetFirst<IBuffableBehavior>();
            ////buffable.AddEnchantments(new IEnchantment[]
            ////{
            ////    new Enchantment(
            ////        new StringIdentifier("stat1"),
            ////        new IComponent[]
            ////        {
            ////            new EnchantmentExpressionComponent(new CalculationPriority<int>(1), "stat1 + 1"),
            ////            new ExpiryTriggerComponent(new DurationTriggerComponent(new Interval<double>(5000))),
            ////            dependencyContainer.Resolve<IAppliesToBaseStat>(),
            ////        }),
            ////});

            ////var item = generatedItems.First();

            ////var buffableItem = item
            ////    .Behaviors
            ////    .GetFirst<IBuffableBehavior>();
            ////buffableItem.AddEnchantments(new IEnchantment[]
            ////{
            ////    new Enchantment(
            ////        new StringIdentifier("stat2"),
            ////        new IComponent[]
            ////        {
            ////            new EnchantmentExpressionComponent(new CalculationPriority<int>(1), "stat2 + 1"),
            ////            new ExpiryTriggerComponent(new DurationTriggerComponent(new Interval<double>(5000))),
            ////        }),
            ////});

            ////var canEquip = actor
            ////    .Behaviors
            ////    .GetFirst<ICanEquipBehavior>();
            ////canEquip.TryEquip(
            ////    new StringIdentifier("left hand"),
            ////    item.Behaviors.GetFirst<ICanBeEquippedBehavior>());

            ////dependencyContainer
            ////    .Resolve<IMutableGameObjectManager>()
            ////    .MarkForAddition(actor);

            ////var cancellationTokenSource = new CancellationTokenSource();
            ////gameEngine.Start(cancellationTokenSource.Token);
            

            var itemGenerators = new IItemGenerator[]
            {
                ////new AlwaysMatchItemGeneratorPlugin(),
                ////new RandomRollItemGeneratorPlugin(
                ////    new StringIdentifier("random-roll"),
                ////    0.80), 
            };

            var itemGeneratorFacade = dependencyContainer.Resolve<IItemGeneratorFacade>();

            var itemGeneratorContext = new GeneratorContext(
                1,
                1,
                new IGeneratorAttribute[]
                {
                    new GeneratorAttribute(
                        new StringIdentifier("affix-type"), 
                        new StringCollectionGeneratorAttributeValue("magic"),
                        false),
                    new GeneratorAttribute(
                        new StringIdentifier("random-roll"),
                        new DoubleGeneratorAttributeValue(dependencyContainer.Resolve<IRandomNumberGenerator>().NextDouble()),
                        false),
                    new GeneratorAttribute(
                        new StringIdentifier("item-level"),
                        new DoubleGeneratorAttributeValue(45),
                        false),
                });

            var items = itemGeneratorFacade
                .GenerateItems(itemGeneratorContext)
                .ToArray();

            Console.ReadLine();
        }
    }
}