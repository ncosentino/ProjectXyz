using ProjectXyz.Data.Resources.Interface.Graphics;
using ProjectXyz.Data.Resources.Interface.Languages;
using ProjectXyz.Data.Resources.Interface.Strings;

namespace ProjectXyz.Data.Resources.Interface
{
    public interface IResourcesDataManager
    {
        #region Properties
        IDisplayLanguageRepository DisplayLanguages { get; }

        IStringResourceRepository StringResources { get; }

        IGraphicResourceRepository GraphicResources { get; }
        #endregion
    }
}
