using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Resources.Core.Graphics;
using ProjectXyz.Data.Resources.Core.Languages;
using ProjectXyz.Data.Resources.Core.Strings;
using ProjectXyz.Data.Resources.Interface;
using ProjectXyz.Data.Resources.Interface.Graphics;
using ProjectXyz.Data.Resources.Interface.Languages;
using ProjectXyz.Data.Resources.Interface.Strings;

namespace ProjectXyz.Data.Resources.Sql
{
    public sealed class SqlResourcesDataManager : IResourcesDataManager
    {
        #region Fields
        private readonly IDisplayLanguageRepository _displayLanguageRepository;
        private readonly IStringResourceRepository _stringResourceRepository;
        private readonly IGraphicResourceRepository _graphicResourceRepository;
        #endregion

        #region Constructors
        private SqlResourcesDataManager(IDatabase database)
        {
            var displayLanguageFactory = DisplayLanguageFactory.Create();
            _displayLanguageRepository = DisplayLanguageRepository.Create(
                database,
                displayLanguageFactory);

            var stringResourceFactory = StringResourceFactory.Create();
            _stringResourceRepository = StringResourceRepository.Create(
                database,
                stringResourceFactory);

            var graphicResourceFactory = GraphicResourceFactory.Create();
            _graphicResourceRepository = GraphicResourceRepository.Create(
                database,
                graphicResourceFactory);
        }
        #endregion

        #region Properties
        public IDisplayLanguageRepository DisplayLanguages { get { return _displayLanguageRepository; } }

        public IStringResourceRepository StringResources { get { return _stringResourceRepository; } }

        public IGraphicResourceRepository GraphicResources { get { return _graphicResourceRepository; } }
        #endregion

        #region Methods
        public static IResourcesDataManager Create(IDatabase database)
        {
            var dataManager = new SqlResourcesDataManager(database);
            return dataManager;
        }
        #endregion
    }
}
