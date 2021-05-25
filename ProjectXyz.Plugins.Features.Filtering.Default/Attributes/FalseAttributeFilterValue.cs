
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Attributes
{
    public sealed class FalseAttributeFilterValue : IFilterAttributeValue
    {
        public override string ToString() => "False";
    }
}