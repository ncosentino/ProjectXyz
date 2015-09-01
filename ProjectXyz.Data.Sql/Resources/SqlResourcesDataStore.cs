using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Resources;
using ProjectXyz.Data.Interface.Resources;

namespace ProjectXyz.Data.Sql.Resources
{
    public sealed class SqlResourcesDataStore : IResourcesDataStore
    {
        #region Fields
        private readonly IDisplayLanguageRepository _displayLanguageRepository;
        private readonly IStringResourceRepository _stringResourceRepository;
        private readonly IGraphicResourceRepository _graphicResourceRepository;
        #endregion

        #region Constructors
        private SqlResourcesDataStore(IDatabase database)
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
        public static IResourcesDataStore Create(IDatabase database)
        {
            var dataStore = new SqlResourcesDataStore(database);
            return dataStore;
        }
        #endregion
    }
}
