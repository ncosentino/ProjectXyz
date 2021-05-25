
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Attributes
{
    public sealed class TrueAttributeFilterValue : IFilterAttributeValue
    {
        public override string ToString() => "True";
    }
}