using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Resources
{
    public interface IResourcesDataStore
    {
        #region Properties
        IDisplayLanguageRepository DisplayLanguages { get; }

        IStringResourceRepository StringResources { get; }

        IGraphicResourceRepository GraphicResources { get; }
        #endregion
    }
}
