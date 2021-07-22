using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Moq;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Enchantments.Generation.Autofac;
using ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory.Autofac;
using ProjectXyz.Plugins.Features.ExpressionEnchantments.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Default.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Tests.Macerus.Plugins.Content.Skills;
using ProjectXyz.Plugins.Features.Triggering.Default.Autofac;
using ProjectXyz.Plugins.Framework.Math.Jace;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Tests
{
    public sealed class EnchantmentIdentifiers : IEnchantmentIdentifiers
    {
        public IIdentifier EnchantmentDefinitionId { get; } = new StringIdentifier("id");
    }

    public sealed class ValueMapperRepository : IValueMapperRepository
    {
        public IEnumerable<ValueMapperDelegate> GetValueMappers()
        {
            yield return context => new KeyValuePair<string, double>("INTERVAL", context.ElapsedTurns);
        }
    }

    public sealed class Logger : ILogger
    {
        public void Debug(string message) =>
            Debug(message, null);

        public void Debug(string message, object data) =>
            Log("DEBUG", message, data);

        public void Error(string message) =>
            Error(message, null);

        public void Error(string message, object data) =>
            Log("ERROR", message, data);

        public void Info(string message) =>
            Info(message, null);

        public void Info(string message, object data) =>
            Log("INFO", message, data);

        public void Warn(string message) =>
            Warn(message, null);

        public void Warn(string message, object data) =>
            Log("WARN", message, data);

        private void Log(string prefix, string message, object data)
        {
            Console.WriteLine($"{prefix}: {message}");
            if (data != null)
            {
                Console.WriteLine($"\t{data}");
            }
        }
    }

    public sealed class SkillRepositoryContainer
    {
        private readonly IDiscoverableSkillDefinitionRepository _skillDefinitionRepository;

        public SkillRepositoryContainer(
            IDiscoverableSkillDefinitionRepository skillDefinitionRepository)
        {
            _skillDefinitionRepository = skillDefinitionRepository;

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new SkillModule());
            containerBuilder.RegisterModule(new Plugins.Enchantments.Stats.Autofac.StatsModule());
            containerBuilder.RegisterModule(new Plugins.Stats.Autofac.StatsModule());
            containerBuilder.RegisterModule(new EnchantmentsGenerationModule());
            containerBuilder.RegisterModule(new InMemoryEnchantmentsModule());
            containerBuilder.RegisterModule(new GenerationModule());
            containerBuilder.RegisterModule(new JaceModule());
            containerBuilder.RegisterModule(new StatesModule());
            containerBuilder.RegisterModule(new Shared.Behaviors.Filtering.Autofac.FilteringModule());
            containerBuilder.RegisterModule(new GameObjectsModule());
            containerBuilder.RegisterModule(new CommonBehaviors.Autofac.CommonBehaviorsModule());
            containerBuilder.RegisterModule(new EnchantmentsModule());
            containerBuilder.RegisterModule(new TriggeringModule());
            containerBuilder
                .RegisterInstance(new EnchantmentIdentifiers())
                .AsImplementedInterfaces();
            containerBuilder
                .RegisterInstance(new ValueMapperRepository())
                .AsImplementedInterfaces();
            containerBuilder
                .RegisterInstance(new Logger())
                .AsImplementedInterfaces();
            containerBuilder
                .Register(c => _skillDefinitionRepository)
                .AsImplementedInterfaces();

            var dependencyContainer = containerBuilder.Build();
            var lifetimeScope = dependencyContainer.BeginLifetimeScope();

            Instance = lifetimeScope;
        }

        private ILifetimeScope Instance { get; }

        public T Resolve<T>() => Instance.Resolve<T>();
    }

    public sealed class SkillRepositoryTests
    {
        private readonly SkillRepositoryContainer _container;
        private readonly ISkillRepository _skillRepository;
        private readonly Mock<IDiscoverableSkillDefinitionRepository> _skillDefinitionRepositoryMock;

        public SkillRepositoryTests()
        {
            _skillDefinitionRepositoryMock = new Mock<IDiscoverableSkillDefinitionRepository>(MockBehavior.Strict);

            _container = new SkillRepositoryContainer(
                _skillDefinitionRepositoryMock.Object);

            _skillRepository = _container.Resolve<ISkillRepository>();
        }

        [Fact]
        private void GetSkills_NoMatch_Empty()
        {
            var filterContext = _container
                .Resolve<IFilterContextFactory>()
                .CreateNoneFilterContext();

            _skillDefinitionRepositoryMock
                .Setup(x => x.GetSkillDefinitions(filterContext))
                .Returns(Enumerable.Empty<ISkillDefinition>());

            var skills = _skillRepository.GetSkills(filterContext);

            Assert.Empty(skills);
        }

        [Fact]
        private void GetSkills_FireballDefinition_ReturnsExpectedGameObjects()
        {
            var filterContext = _container
                .Resolve<IFilterContextFactory>()
                .CreateNoneFilterContext();

            _skillDefinitionRepositoryMock
                .Setup(x => x.GetSkillDefinitions(filterContext))
                .Returns(
                    SkillDefinition
                        .FromId("fireball")
                        .WithDisplayName("Fireball")
                        .WithDisplayIcon(@"graphics\skills\fireball")
                        .WithResourceRequirement(4, 10)
                        .CanBeUsedInCombat()
                        .IsACombinationOf(
                            ExecuteSkills.InParallel(
                                SkillDefinition
                                    .Anonymous()
                                    .Enchant("increase-fire-damage")
                                    .AffectsTeams(0)
                                    .StartsAtOffsetFromUser(0, 0)
                                    .TargetsPattern()),
                            ExecuteSkills.InSequence(
                                SkillDefinition
                                    .Anonymous()
                                    .InflictDamage()
                                    .AffectsTeams(1)
                                    .StartsAtOffsetFromUser(0, 1)
                                    .TargetsPattern(
                                        Tuple.Create(0, 1))))
                        .End());

            var skills = _skillRepository
                .GetSkills(filterContext)
                .ToArray();

            Assert.Equal(3, skills.Count());

            var fireball = skills[2];

            Assert.True(fireball.Has<IUseInCombatBehavior>());
            Assert.True(fireball.GetOnly<IHasDisplayNameBehavior>().DisplayName == "Fireball");
            Assert.True(fireball.GetOnly<IHasDisplayIconBehavior>().IconResourceId.ToString() == @"graphics\skills\fireball");
            Assert.True(fireball.GetOnly<ISkillResourceUsageBehavior>().StaticStatRequirements[new IntIdentifier(4)] == 10);

            var fireDamageEnchant = skills[0];

            Assert.Contains(
                new StringIdentifier("increase-fire-damage"),
                fireDamageEnchant.GetOnly<IApplyEnchantmentsBehavior>().EnchantmentDefinitionIds);
            Assert.Contains(
                new IntIdentifier(0),
                fireDamageEnchant.GetOnly<ITargetCombatTeamBehavior>().AffectedTeamIds);
            Assert.True(fireDamageEnchant.GetOnly<ITargetOriginBehavior>().OffsetFromCasterX == 0);
            Assert.True(fireDamageEnchant.GetOnly<ITargetOriginBehavior>().OffsetFromCasterY == 0);
            Assert.Empty(fireDamageEnchant.GetOnly<ITargetPatternBehavior>().LocationsOffsetFromOrigin);

            var damage = skills[1];

            Assert.True(damage.Has<IInflictDamageBehavior>());
            Assert.Contains(
                new IntIdentifier(1),
                damage.GetOnly<ITargetCombatTeamBehavior>().AffectedTeamIds);
            Assert.True(damage.GetOnly<ITargetOriginBehavior>().OffsetFromCasterX == 0);
            Assert.True(damage.GetOnly<ITargetOriginBehavior>().OffsetFromCasterY == 1);
            Assert.Contains(
                Tuple.Create(0, 1),
                damage.GetOnly<ITargetPatternBehavior>().LocationsOffsetFromOrigin);
        }
    }
}