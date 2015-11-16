using System;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Resources.Interface.Strings;

namespace ProjectXyz.Plugins.Items.Normal.Tests.Integration.Helpers
{
    public static class StringResourceRepositoryHelper
    {
        #region Methods
        public static IStringResource AddStringResource(IDataManager dataManager, string value)
        {
            var displayLanguage = dataManager.Resources.DisplayLanguages.Add(
                Guid.NewGuid(),
                Guid.NewGuid().ToString());

            var stringResource = AddStringResource(dataManager, displayLanguage.Id, value);
            return stringResource;
        }

        public static IStringResource AddStringResource(IDataManager dataManager, Guid displayLanguageId, string value)
        {
            var stringResource = dataManager.Resources.StringResources.Add(
                Guid.NewGuid(),
                displayLanguageId,
                value);

            return stringResource;
        }
        #endregion
    }
}
