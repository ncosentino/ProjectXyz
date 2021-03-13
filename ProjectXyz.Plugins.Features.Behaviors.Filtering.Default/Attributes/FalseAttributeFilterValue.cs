
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes
{
    public sealed class FalseAttributeFilterValue : IFilterAttributeValue
    {
        public override string ToString() => "False";
    }
}