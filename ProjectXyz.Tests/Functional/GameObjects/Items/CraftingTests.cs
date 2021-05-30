using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Filtering;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Default;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Tests.Functional.GameObjects.Items
{
    public sealed class CraftingTests
    {
        private static IItemFactory _itemFactory;
        private static ICraftingHandlerFacade _craftingHandlerFacade;

        static CraftingTests()
        {
            _itemFactory = CachedDependencyLoader.LifeTimeScope.Resolve<IItemFactory>();
            _craftingHandlerFacade = CachedDependencyLoader.LifeTimeScope.Resolve<ICraftingHandlerFacade>();
        }

        [Fact]
        private void TryHandle_MeetsReplacementDefinition_SuccessAndNewItemReturned()
        {
            var ingredient1 = _itemFactory.Create(
                new TagsBehavior(new StringIdentifier("Ingredient1")));
            var ingredient2 = _itemFactory.Create(
                new TagsBehavior(new StringIdentifier("Ingredient2")));

            Assert.True(_craftingHandlerFacade.TryHandle(
                new IFilterAttribute[] { },
                new[]
                {
                    ingredient1,
                    ingredient2,
                },
                out var craftedItems));
            Assert.NotNull(craftedItems);
            Assert.Equal(3, craftedItems.Count);
        }

        [Fact]
        private void TryHandle_DoesNotMeetAnyDefinition_ReturnsFalseNoItems()
        {
            var ingredient1 = _itemFactory.Create(
                new TagsBehavior(new StringIdentifier(Guid.NewGuid().ToString())));
            var ingredient2 = _itemFactory.Create(
                new TagsBehavior(new StringIdentifier(Guid.NewGuid().ToString())));

            Assert.False(_craftingHandlerFacade.TryHandle(
                new IFilterAttribute[] { },
                new[]
                {
                    ingredient1,
                    ingredient2,
                },
                out var craftedItems));
            Assert.Null(craftedItems);
        }
    }

    public sealed class CraftingTestModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(x => new InMemoryReplaceIngredientsCraftingRepository(new[]
                {
                    new ReplaceIngredientsCraftingDefinition(
                        new IFilterAttribute[] { },
                        new[]
                        {
                            new IFilterAttributeValue[]
                            {
                                new AnyTagsFilter(new StringIdentifier("Ingredient1")),
                            },
                            new IFilterAttributeValue[]
                            {
                                new AnyTagsFilter(new StringIdentifier("Ingredient2")),
                            },
                        },
                        new[]
                        {
                            new StringIdentifier("Table B")
                        }),
                }))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
