using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes
{
    /// <summary>
    /// These extensions are not located with the API interfaces because they 
    /// rely on the concrete type <see cref="FilterAttribute"/>.
    /// </summary>
    public static class FilterAttributeExtensionMethods
    {
        public static IFilterAttribute CopyWithRequired(
            this IFilterAttribute filterAttribute,
            bool required)
        {
            var copy = new FilterAttribute(
                filterAttribute.Id,
                filterAttribute.Value,
                required);
            return copy;
        }
    }
}