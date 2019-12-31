using System;
using System.Linq;
using ProjectXyz.Game.Core.Autofac;

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
                ////.Discover(moduleDirectory, "*.Autofac.dll"))
                ////.Concat(moduleDiscoverer
                ////.Discover(moduleDirectory, "Examples.Modules.*.dll"));
                .Discover(moduleDirectory, "*.dll"));
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

            Console.ReadLine();
        }
    }
}