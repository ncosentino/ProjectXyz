internal sealed class Program
{
    public static void Main()
    {

    }
}

////using System;
////using System.Collections.Generic;
////using System.Data;
////using System.Globalization;
////using System.IO;
////using System.Linq;
////using System.Reflection;
////using System.Text;
////using System.Threading.Tasks;
////using ProjectXyz.Application.Core.Enchantments;
////using ProjectXyz.Application.Core.Stats.Calculations;
////using ProjectXyz.Application.Core.Weather;
////using ProjectXyz.Application.Interface.Enchantments;
////using ProjectXyz.Application.Interface.Stats;
////using ProjectXyz.Application.Interface.Stats.Calculations;
////using ProjectXyz.Application.Shared.Stats;
////using ProjectXyz.Framework.Interface;
////using ProjectXyz.Framework.Interface.Collections;
////using ProjectXyz.Framework.Shared;
////using ProjectXyz.Framework.Shared.Math;
////using ProjectXyz.Plugins.Api.Enchantments;
////using ProjectXyz.Plugins.Core;
////using ProjectXyz.Plugins.Core.Enchantments;
////using ProjectXyz.Plugins.Interface;

////namespace ConsoleApplication1
////{
////    internal sealed class Program
////    {
////        #region Methods
////        private static int Main(string[] args)
////        {
////            var debuffId = new StringIdentifier("Debuff");
////            var buffId = new StringIdentifier("Buff");
////            var blessingId = new StringIdentifier("Blessing");

////            var speedId = new StringIdentifier("Speed");
////            var strengthId = new StringIdentifier("Strength");
////            var goldId = new StringIdentifier("Gold");

////            var equipmentId = new StringIdentifier("Equipment");
////            var weatherId = new StringIdentifier("Weather");
////            var timeOfDayId = new StringIdentifier("Time of Day");

////            var equippedId = new StringIdentifier("Equipped");
////            var inInventoryId = new StringIdentifier("In Inventory");

////            var nightId = new StringIdentifier("Night");
////            var dayId = new StringIdentifier("Day");

////            var rainId = new StringIdentifier("Rain");
////            var sunnyId = new StringIdentifier("Sunny");

////            var hitEventId = new StringIdentifier("On Hit");

////            var baseStats = new Dictionary<IIdentifier, IStat>();

////            var strengthBuff = Enchantment
////                .Create(strengthId, 5)
////                .As(buffId)
////                .For(TimeSpan.FromSeconds(5));

////            var permanentStrengthBoost = Enchantment
////                .Create(strengthId, 5)
////                .Permanent();

////            var strengthBlessing = Enchantment
////                .Create(strengthId, 5)
////                .As(blessingId)
////                .For(TimeSpan.FromSeconds(5));

////            var nightStealthBlessing = Enchantment
////                .Create(speedId, 5)
////                .As(blessingId)
////                .While(timeOfDayId)
////                .Is(nightId);

////            var rainSlowdownDebuff = Enchantment
////                .Create(speedId, -5)
////                .As(debuffId)
////                .While(weatherId)
////                .Is(rainId);

////            var goldOnHitWeaponBonus = Enchantment
////                .Create(goldId, 5)
////                .While(equipmentId)
////                .Is(equippedId)
////                .While(timeOfDayId)
////                .Is(dayId)
////                .While(weatherId)
////                .Is(sunnyId)
////                .TriggerOn(hitEventId);

////            return 0;
////        }

////        public interface IStateLookup
////        {
////            double IsActive(IIdentifier stateIdentifier);
////        }

////        public sealed class StatCalculator
////        {
////            private readonly IStatCalculator _statCalculator;
////            private readonly IReadOnlyDictionary<IIdentifier, IStateLookup> _stateLookupMapping;

////            public StatCalculator(IReadOnlyDictionary<IIdentifier, IStateLookup> stateLookupMapping)
////            {
////                _stateLookupMapping = stateLookupMapping;
////            }

////            public void Calculate(
////                IIdentifier statDefinitionId,
////                IReadOnlyDictionary<IIdentifier, IStat> baseStats,
////                IEnumerable<IEnchantment> enchantments)
////            {
////                var baseValue = _statCalculator.Calculate(
////                    statDefinitionId,
////                    baseStats);
////            }
////        }

////        public interface IEnchantmentBuilderWhile
////        {
////            IEnchantmentBuilder Is(IIdentifier stateIdentifier);
////        }

////        public interface IEnchantmentBuilder
////        {
////            IEnchantmentBuilderWhile While(IIdentifier stateTypeIdentifier);

////            IEnchantmentBuilder As(IIdentifier modifierId);

////            IEnchantmentBuilder For(TimeSpan duration);

////            IEnchantmentBuilder Permanent();
////        }

////        public static class Enchantment
////        {
////            public static IEnchantmentBuilder Create(IIdentifier statDefinitionId, double value)
////            {
                
////            }
////        }

////        public interface IEnchantment
////        {
////            int CalculationPriority { get; }
////        }
////        #endregion

////        ////public sealed class Class1
////        ////{
////        ////    public void DoTheThing()
////        ////    {
////        ////        var @try = Try.InReleaseMode;

////        ////        var statCollectionFactory = new StatCollectionFactory();
////        ////        var stats = statCollectionFactory.Create(new[]
////        ////        {
////        ////            new Stat(new IntIdentifier(0), 123),
////        ////            new Stat(new IntIdentifier(1), 456),
////        ////            new Stat(new IntIdentifier(2), 789),
////        ////        });

////        ////        var stat = stats[new IntIdentifier(1)];
////        ////        var stat2 = stats[new IntIdentifier(new Identifier<int>(1))];
////        ////        var stat3 = stats[new Identifier<int>(1)];

////        ////        Console.WriteLine(stat);
////        ////        Console.WriteLine(stat2);
////        ////        Console.WriteLine(stat3);
////        ////        Console.ReadLine();

////        ////        var pluginProvider = CreatePluginProvider(@try);
////        ////        var calculator = CreateEnchantmentCalculator(
////        ////            @try,
////        ////            pluginProvider);

////        ////        var enchantments = Enumerable.Empty<IEnchantment>();
////        ////        var result = calculator.Calculate(
////        ////            stats,
////        ////            enchantments);
////        ////    }

////        ////    private IPluginProvider CreatePluginProvider(ITry @try)
////        ////    {
////        ////        var assemblyPluginRepositoryFactory = new AssemblyPluginRepositoryFactory(
////        ////            @try,
////        ////            x => Directory.GetFiles(x, "*.plugins.*.dll", SearchOption.AllDirectories),
////        ////            Assembly.LoadFrom,
////        ////            new[] { AppDomain.CurrentDomain.BaseDirectory });

////        ////        var weatherManager = new WeatherManager();
////        ////        var enchantmentPluginInitializationProvider = new EnchantmentPluginInitializationProvider(weatherManager);
////        ////        var enchantmentPluginLoader = new EnchantmentPluginAssemblyLoader(enchantmentPluginInitializationProvider);
////        ////        var pluginRepositories = new[]
////        ////        {
////        ////            assemblyPluginRepositoryFactory.Create(enchantmentPluginLoader.LoadFromAssembly),
////        ////        };
////        ////        var pluginProvider = new PluginProvider(pluginRepositories);
////        ////        return pluginProvider;
////        ////    }

////        ////    private IEnchantmentCalculator CreateEnchantmentCalculator(
////        ////        ITry @try,
////        ////        IPluginProvider pluginProvider)
////        ////    {
////        ////        var enchantmentCalculatorResultFactory = new EnchantmentCalculatorResultFactory();
////        ////        var enchantmentContext = new EnchantmentContext();
////        ////        var enchantmentTypeCalculators = GetEnchantmentTypeCalculators(pluginProvider).ToArray();
////        ////        var calculator = new EnchantmentCalculator(
////        ////            @try,
////        ////            enchantmentCalculatorResultFactory,
////        ////            enchantmentContext,
////        ////            enchantmentTypeCalculators);
////        ////        return calculator;
////        ////    }

////        ////    private IEnumerable<IEnchantmentTypeCalculator> GetEnchantmentTypeCalculators(IPluginProvider pluginProvider)
////        ////    {
////        ////        var enchantmentPlugins = pluginProvider.GetPlugins<IEnchantmentPlugin>();
////        ////        return enchantmentPlugins.Select(x => x.EnchantmentTypeCalculator);
////        ////    }
////        ////}
////    }
////}
