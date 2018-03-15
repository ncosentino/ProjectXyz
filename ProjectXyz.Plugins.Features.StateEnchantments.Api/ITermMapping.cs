using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.StateEnchantments.Api
{
    public interface ITermMapping : IReadOnlyDictionary<IIdentifier, string>
    {
    }
}