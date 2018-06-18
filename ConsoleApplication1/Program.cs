using System;
using System.Linq;
using Autofac;
using ConsoleApplication1.Wip.Items.Generation;
using ConsoleApplication1.Wip.Items.Generation.Plugins;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Items;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Items.Generation;
using ProjectXyz.Shared.Game.Items.Generation.InMemory;
using ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes;
using IItemGenerator = ProjectXyz.Api.Items.Generation.IItemGenerator;

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
                .Discover(moduleDirectory, "*.Dependencies.Autofac.dll"))
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

            ////var gameEngine = dependencyContainer.Resolve<IGameEngine>();

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
            
            var attributeValueMatchFacade = new AttributeValueMatchFacade();

            attributeValueMatchFacade.Register<
                StringItemGeneratorAttributeValue,
                StringCollectionItemGeneratorAttributeValue>(
                (v1, v2) =>
                {
                    var isAttrtMatch = v2
                        .Values
                        .Contains(v1.Value);
                    return isAttrtMatch;
                });
            attributeValueMatchFacade.Register<
                StringItemGeneratorAttributeValue,
                StringItemGeneratorAttributeValue>(
                (v1, v2) =>
                {
                    var isAttrtMatch = v2.Value.Equals(v1.Value);
                    return isAttrtMatch;
                });
            attributeValueMatchFacade.Register<
                RangeItemGeneratorAttributeValue,
                DoubleItemGeneratorAttributeValue> (
                (v1, v2) =>
                {
                    var isAttrtMatch = 
                        v1.Minimum <= v2.Value &&
                        v1.Maximum >= v2.Value;
                    return isAttrtMatch;
                });

            var activeEnchantmentManagerFactory = dependencyContainer.Resolve<IActiveEnchantmentManagerFactory>();
            var attributeFilterer = new InMemoryAttributeFilterer(attributeValueMatchFacade);

            var itemGeneratorComponentToBehaviorConverterFacade = new ItemGeneratorComponentToBehaviorConverterFacade();
            itemGeneratorComponentToBehaviorConverterFacade.Register<ItemGeneratorComponent>(_ =>
            {
                var activeEnchantmentManager = activeEnchantmentManagerFactory.Create();

                return new IBehavior[]
                {
                    new CanBeEquippedBehavior(),
                    new BuffableBehavior(activeEnchantmentManager),
                    new HasEnchantmentsBehavior(activeEnchantmentManager),
                };
            });

            var itemDefinitionRepository = new InMemoryItemDefinitionRepository(
                attributeFilterer,
                new IItemDefinition[]
                {
                    new ItemDefinition(
                        new[]
                        {
                            new ItemGeneratorAttribute(
                                new StringIdentifier("item-level"),
                                new RangeItemGeneratorAttributeValue(40, 50)),
                        },
                        new[]
                        {
                            new ItemGeneratorComponent(),
                        }),
                });

            var baseItemGenerator = new BaseItemGenerator(
                dependencyContainer.Resolve<IItemFactory>(),
                dependencyContainer.Resolve<IRandomNumberGenerator>(),
                itemDefinitionRepository,
                itemGeneratorComponentToBehaviorConverterFacade);

            var itemGenerators = new IItemGenerator[]
            {
                new NormalItemGeneratorPlugin(baseItemGenerator), 
                ////new MagicItemGeneratorPlugin(baseItemGenerator),
                ////new AlwaysMatchItemGeneratorPlugin(),
                ////new RandomRollItemGeneratorPlugin(
                ////    new StringIdentifier("random-roll"),
                ////    0.80), 
            };

            var itemGeneratorFacade = new ItemGeneratorFacade(
                attributeFilterer,
                itemGenerators);

            var itemGeneratorContext = new ItemGeneratorContext(
                1,
                1,
                new IItemGeneratorAttribute[]
                {
                    new ItemGeneratorAttribute(
                        new StringIdentifier("affix-type"), 
                        new StringCollectionItemGeneratorAttributeValue("normal")),
                    new ItemGeneratorAttribute(
                        new StringIdentifier("random-roll"),
                        new DoubleItemGeneratorAttributeValue(dependencyContainer.Resolve<IRandomNumberGenerator>().NextDouble())),
                    new ItemGeneratorAttribute(
                        new StringIdentifier("item-level"),
                        new DoubleItemGeneratorAttributeValue(45)),
                });

            var items = itemGeneratorFacade
                .GenerateItems(itemGeneratorContext)
                .ToArray();

            Console.ReadLine();
        }
    }
}