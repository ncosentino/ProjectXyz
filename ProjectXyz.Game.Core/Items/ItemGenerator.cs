using System.Collections.Generic;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.Entities.Extensions;
using ProjectXyz.Framework.Extensions;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.GameObjects.Items;

namespace ProjectXyz.Game.Core.Items
{
    public sealed class ItemGenerator : IItemGenerator
    {
        private readonly IItemFactory _itemFactory;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public ItemGenerator(
            IItemFactory itemFactory,
            IRandomNumberGenerator randomNumberGenerator)
        {
            _itemFactory = itemFactory;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public IEnumerable<IGameObject> GenerateItems(IItemGenerationContext itemGenerationContext)
        {
            var count = GetCount(itemGenerationContext.Components);

            for (int i = 0; i < count; i++)
            {
                var item = _itemFactory.Create();
                yield return item;
            }
        }

        private int GetCount(IComponentCollection components)
        {
            // FIXME: this core code shouldn't be the one responsible for 
            // knowing about different components and how to handle them
            var itemCountContextComponent = components.GetFirst<IItemCountContextComponent>();
            var count = _randomNumberGenerator.NextInRange(
                itemCountContextComponent.Minimum,
                itemCountContextComponent.Maximum);
            return count;
        }
    }
}