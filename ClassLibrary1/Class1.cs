using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using ClassLibrary1.Application.Core.Enchantments;
using ClassLibrary1.Application.Core.Weather;
using ClassLibrary1.Application.Interface.Enchantments;
using ClassLibrary1.Application.Shared.Stats;
using ClassLibrary1.Framework.Interface;
using ClassLibrary1.Framework.Shared;
using ClassLibrary1.Plugins.Api.Enchantments;
using ClassLibrary1.Plugins.Core;
using ClassLibrary1.Plugins.Core.Enchantments;
using ClassLibrary1.Plugins.Interface;

namespace ClassLibrary1
{
    public sealed class Class1
    {
        public void DoTheThing()
        {
            var @try = Try.InReleaseMode;

            var statCollectionFactory = new StatCollectionFactory();
            var stats = statCollectionFactory.Create(new[]
            {
                new Stat(new IntIdentifier(0), 123),
                new Stat(new IntIdentifier(1), 456),
                new Stat(new IntIdentifier(2), 789),
            });

            var stat = stats[new IntIdentifier(1)];
            var stat2 = stats[new IntIdentifier(new Identifier<int>(1))];
            var stat3 = stats[new Identifier<int>(1)];

            Console.WriteLine(stat);
            Console.WriteLine(stat2);
            Console.WriteLine(stat3);
            Console.ReadLine();

            var pluginProvider = CreatePluginProvider(@try);
            var calculator = CreateEnchantmentCalculator(
                @try,
                pluginProvider);

            var enchantments = Enumerable.Empty<IEnchantment>();
            var result = calculator.Calculate(
                stats,
                enchantments);
        }

        private IPluginProvider CreatePluginProvider(ITry @try)
        {
            var assemblyPluginRepositoryFactory = new AssemblyPluginRepositoryFactory(
                @try,
                x => Directory.GetFiles(x, "*.dll"),
                Assembly.LoadFrom,
                new[] { AppDomain.CurrentDomain.BaseDirectory });

            var weatherManager = new WeatherManager();
            var enchantmentPluginInitializationProvider = new EnchantmentPluginInitializationProvider(weatherManager);
            var enchantmentPluginLoader = new EnchantmentPluginAssemblyLoader(enchantmentPluginInitializationProvider);
            var pluginRepositories = new[]
            {
                assemblyPluginRepositoryFactory.Create(enchantmentPluginLoader.LoadFromAssembly),
            };
            var pluginProvider = new PluginProvider(pluginRepositories);
            return pluginProvider;
        }

        private IEnchantmentCalculator CreateEnchantmentCalculator(
            ITry @try,
            IPluginProvider pluginProvider)
        {
            var enchantmentCalculatorResultFactory = new EnchantmentCalculatorResultFactory();
            var enchantmentContext = new EnchantmentContext();
            var enchantmentTypeCalculators = GetEnchantmentTypeCalculators(pluginProvider).ToArray();
            var calculator = new EnchantmentCalculator(
                @try,
                enchantmentCalculatorResultFactory,
                enchantmentContext,
                enchantmentTypeCalculators);
            return calculator;
        }

        private IEnumerable<IEnchantmentTypeCalculator> GetEnchantmentTypeCalculators(IPluginProvider pluginProvider)
        {
            var enchantmentPlugins = pluginProvider.GetPlugins<IEnchantmentPlugin>();
            return enchantmentPlugins.Select(x => x.EnchantmentTypeCalculator);
        }
    }
}
