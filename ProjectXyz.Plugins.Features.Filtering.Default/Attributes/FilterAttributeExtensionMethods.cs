﻿using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Attributes
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