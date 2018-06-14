using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Items;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Framework.Extensions;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Shared.Game.Items.Generation
{
    public sealed class BaseItemGenerator : IBaseItemGenerator
    {
        private readonly IItemFactory _itemFactory;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public BaseItemGenerator(
            IItemFactory itemFactory,
            IRandomNumberGenerator randomNumberGenerator)
        {
            _itemFactory = itemFactory;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public IEnumerable<IGameObject> GenerateItems(IItemGeneratorContext itemGeneratorContext)
        {
            var count = GetCount(
                itemGeneratorContext.MinimumGenerateCount,
                itemGeneratorContext.MaximumGenerateCount);

            for (var i = 0; i < count; i++)
            {
                var item = _itemFactory.Create();
                yield return item;
            }
        }

        private int GetCount(
            int itemCountMinimum,
            int itemCountMaximum)
        {
            var count = _randomNumberGenerator.NextInRange(
                itemCountMinimum,
                itemCountMaximum);
            return count;
        }
    }
}