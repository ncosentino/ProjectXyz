using System.Collections.Generic;

namespace ProjectXyz.Api.Items
{
    public interface IItemGenerationContextComponentProvider
    {
        IEnumerable<IItemGenerationContextComponent> CreateComponents();
    }
}