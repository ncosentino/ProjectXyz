using System;
using System.Linq;
using Autofac;
using ConsoleApplication1.Wip.Items.Generation;
using ConsoleApplication1.Wip.Items.Generation.Plugins;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.GameObjects.Items;
using ProjectXyz.Api.GameObjects.Items.Generation;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory;
using ProjectXyz.Shared.Game.GameObjects.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Game.GameObjects.Items.Generation;
using ProjectXyz.Shared.Game.GameObjects.Items.Generation.InMemory;
using IItemGenerator = ProjectXyz.Api.GameObjects.Items.Generation.IItemGenerator;

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
                StringGeneratorAttributeValue,
                StringCollectionGeneratorAttributeValue>(
                (v1, v2) =>
                {
                    var isAttrtMatch = v2
                        .Values
                        .Contains(v1.Value);
                    return isAttrtMatch;
                });
            attributeValueMatchFacade.Register<
                StringGeneratorAttributeValue,
                StringGeneratorAttributeValue>(
                (v1, v2) =>
                {
                    var isAttrtMatch = v2.Value.Equals(v1.Value);
                    return isAttrtMatch;
                });
            attributeValueMatchFacade.Register<
                RangeGeneratorAttributeValue,
                DoubleGeneratorAttributeValue> (
                (v1, v2) =>
                {
                    var isAttrtMatch = 
                        v1.Minimum <= v2.Value &&
                        v1.Maximum >= v2.Value;
                    return isAttrtMatch;
                });

            var activeEnchantmentManagerFactory = dependencyContainer.Resolve<IActiveEnchantmentManagerFactory>();
            var attributeFilterer = new InMemoryAttributeFilterer(attributeValueMatchFacade);

            var itemGeneratorComponentToBehaviorConverterFacade = new GeneratorComponentToBehaviorConverterFacade();
            itemGeneratorComponentToBehaviorConverterFacade.Register<GeneratorComponent>(_ =>
            {
                var activeEnchantmentManager = activeEnchantmentManagerFactory.Create();

                return new IBehavior[]
                {
                    new CanBeEquippedBehavior(),
                    new BuffableBehavior(activeEnchantmentManager),
                    new HasEnchantmentsBehavior(activeEnchantmentManager),
                    new EnchantableBehavior(activeEnchantmentManager), 
                };
            });

            var enchantmentGeneratorComponentToBehaviorConverterFacade = new GeneratorComponentToBehaviorConverterFacade();
            enchantmentGeneratorComponentToBehaviorConverterFacade.Register<GeneratorComponent>(_ =>
            {
                return new IBehavior[]
                {
                    new HasStatDefinitionIdBehavior()
                    {
                        StatDefinitionId = new StringIdentifier("life-stat"),
                    },
                };
            });

            var itemDefinitionRepository = new InMemoryItemDefinitionRepository(
                attributeFilterer,
                new IItemDefinition[]
                {
                    new ItemDefinition(
                        new[]
                        {
                            new GeneratorAttribute(
                                new StringIdentifier("item-level"),
                                new RangeGeneratorAttributeValue(40, 50)),
                        },
                        new[]
                        {
                            new GeneratorComponent(),
                        }),
                });

            var enchantmentDefinitionRepository = new InMemoryEnchantmentDefinitionRepository(
                attributeFilterer,
                new IEnchantmentDefinition[]
                {
                    new EnchantmentDefinition(
                        new[]
                        {
                            new GeneratorAttribute(
                                new StringIdentifier("item-level"),
                                new RangeGeneratorAttributeValue(40, 50)),
                        },
                        new[]
                        {
                            new GeneratorComponent(),
                        }),
                });

            var baseItemGenerator = new BaseItemGenerator(
                dependencyContainer.Resolve<IItemFactory>(),
                dependencyContainer.Resolve<IRandomNumberGenerator>(),
                itemDefinitionRepository,
                itemGeneratorComponentToBehaviorConverterFacade);

            var enchantmentGenerators = new IEnchantmentGenerator[]
            { 
                new MagicEnchantmentGeneratorPlugin(new BaseEnchantmentGenerator(
                    dependencyContainer.Resolve<IEnchantmentFactory>(),
                    dependencyContainer.Resolve<IRandomNumberGenerator>(),
                    enchantmentDefinitionRepository,
                    enchantmentGeneratorComponentToBehaviorConverterFacade)), 
            };

            var enchantmentGenerator = new EnchantmentGeneratorFacade(
                attributeFilterer,
                enchantmentGenerators);

            var itemGenerators = new IItemGenerator[]
            {
                ////new NormalItemGeneratorPlugin(baseItemGenerator), 
                new MagicItemGeneratorPlugin(
                    baseItemGenerator,
                    enchantmentGenerator),
                ////new AlwaysMatchItemGeneratorPlugin(),
                ////new RandomRollItemGeneratorPlugin(
                ////    new StringIdentifier("random-roll"),
                ////    0.80), 
            };

            var itemGeneratorFacade = new ItemGeneratorFacade(
                attributeFilterer,
                itemGenerators);

            var itemGeneratorContext = new GeneratorContext(
                1,
                1,
                new IGeneratorAttribute[]
                {
                    new GeneratorAttribute(
                        new StringIdentifier("affix-type"), 
                        new StringCollectionGeneratorAttributeValue("magic")),
                    new GeneratorAttribute(
                        new StringIdentifier("random-roll"),
                        new DoubleGeneratorAttributeValue(dependencyContainer.Resolve<IRandomNumberGenerator>().NextDouble())),
                    new GeneratorAttribute(
                        new StringIdentifier("item-level"),
                        new DoubleGeneratorAttributeValue(45)),
                });

            var items = itemGeneratorFacade
                .GenerateItems(itemGeneratorContext)
                .ToArray();

            Console.ReadLine();
        }
    }
}