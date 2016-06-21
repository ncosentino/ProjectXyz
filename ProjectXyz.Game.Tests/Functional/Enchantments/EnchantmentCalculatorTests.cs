using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Application.Shared.Stats;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Framework.Testing;
using ProjectXyz.Game.Core.Plugins;
using ProjectXyz.Plugins.Api.Enchantments;
using ProjectXyz.Plugins.Enchantments.Expressions;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class EnchantmentCalculatorTests
    {
        private readonly IIdentifier NO_ID = new IntIdentifier(-1);
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly IExpressionEnchantmentFactory _expressionEnchantmentFactory;

        public EnchantmentCalculatorTests()
        {
            var pluginProviderFactory = new AssemblyPluginProviderFactory(
                Try.FailOnError,
                "*.plugins.*.dll",
                true,
                AppDomain.CurrentDomain.BaseDirectory);
            var pluginProvider = pluginProviderFactory.Create();

            var enchantmentCalculatorResultFactory = new EnchantmentCalculatorResultFactory();
            var enchantmentContext = new EnchantmentContext();
            var calculators = pluginProvider
                .GetPlugins<IEnchantmentPlugin>()
                .Select(x => x.EnchantmentTypeCalculator)
                .ToArray();

            _enchantmentCalculator = new EnchantmentCalculator(
                Try.FailOnError,
                enchantmentCalculatorResultFactory,
                enchantmentContext,
                calculators);

            _expressionEnchantmentFactory = new ExpressionEnchantmentFactory();
        }

        [Fact]
        private void SimpleStat_SingleAssignment_ValueIsSet()
        {
            // Setup
            var stat = new Stat(new IntIdentifier(1), 0);

            var stats = new StatCollectionFactory().Create(stat.Yield());

            var enchantment = _expressionEnchantmentFactory.CreateAssignment(
                NO_ID,
                NO_ID,
                NO_ID,
                stat.StatDefinitionId,
                10,
                0);
            var enchantments = enchantment.AsArray();

            // Execute
            var result = _enchantmentCalculator.Calculate(
                stats,
                enchantments);

            // Assert
            AssertEx.Equal(
                1,
                stats.Count,
                "Expecting only a single stat.");
            AssertEx.Equal(
                10,
                result.Stats[stat.StatDefinitionId].Value,
                "The value of the stat was not as expected.");
            AssertEx.Equal(
                (object)enchantments.Single(),
                result.Enchantments.Single(),
                "Expecting only a single enchantment.");
        }

        [Fact]
        private void SimpleStat_MultipleAssignment_ValueIsSet()
        {
            // Setup
            var stat = new Stat(new IntIdentifier(1), 0);

            var stats = new StatCollectionFactory().Create(stat.Yield());

            var enchantment = _expressionEnchantmentFactory.CreateAssignment(
                NO_ID,
                NO_ID,
                NO_ID,
                stat.StatDefinitionId,
                10,
                0);
            var enchantments = new IEnchantment[]
            {
                enchantment,
                enchantment,
            };

            // Execute
            var result = _enchantmentCalculator.Calculate(
                stats,
                enchantments);

            // Assert
            AssertEx.Equal(
                1,
                stats.Count,
                "Expecting only a single stat.");
            AssertEx.Equal(
                10,
                result.Stats[stat.StatDefinitionId].Value,
                "The value of the stat was not as expected.");
            AssertEx.Equal(
                2,
                result.Enchantments.Count,
                "Expecting multiple enchantments.");
        }

        [Fact]
        private void SimpleStat_SingleAdditive_ValueIsSet()
        {
            // Setup
            var stat = new Stat(new IntIdentifier(1), 0);

            var stats = new StatCollectionFactory().Create(stat.Yield());

            var enchantment = _expressionEnchantmentFactory.CreateAdditive(
                NO_ID,
                NO_ID,
                NO_ID,
                stat.StatDefinitionId,
                10,
                0);
            var enchantments = enchantment.AsArray();

            // Execute
            var result = _enchantmentCalculator.Calculate(
                stats,
                enchantments);

            // Assert
            AssertEx.Equal(
                1,
                stats.Count,
                "Expecting only a single stat.");
            AssertEx.Equal(
                10, 
                result.Stats[stat.StatDefinitionId].Value,
                "The value of the stat was not as expected.");
            AssertEx.Equal(
                (object)enchantments.Single(),
                result.Enchantments.Single(),
                "Expecting only a single enchantment.");
        }

        [Fact]
        private void SimpleStat_MultipleAdditive_ValuesAreAdded()
        {
            // Setup
            var stat = new Stat(new IntIdentifier(1), 0);

            var stats = new StatCollectionFactory().Create(stat.Yield());

            var enchantment = _expressionEnchantmentFactory.CreateAdditive(
                NO_ID,
                NO_ID,
                NO_ID,
                stat.StatDefinitionId,
                10,
                0);

            var enchantments = new IEnchantment[]
            {
                enchantment,
                enchantment,
            };

            // Execute
            var result = _enchantmentCalculator.Calculate(
                stats,
                enchantments);

            // Assert
            AssertEx.Equal(
                1,
                stats.Count,
                "Expecting only a single stat.");
            AssertEx.Equal(
                20,
                result.Stats[stat.StatDefinitionId].Value,
                "The value of the stat was not as expected.");
            AssertEx.Equal(
                2,
                result.Enchantments.Count,
                "Expecting multiple enchantments.");
        }
    }
}
