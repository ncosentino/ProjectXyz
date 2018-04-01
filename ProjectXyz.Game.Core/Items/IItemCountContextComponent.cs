using ProjectXyz.Api.Items;

namespace ProjectXyz.Game.Core.Items
{
    public interface IItemCountContextComponent : IItemGenerationContextComponent
    {
        int Minimum { get; }

        int Maximum { get; }
    }
}