using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Resources.Interface.Strings;

namespace ProjectXyz.Data.Resources.Core.Strings
{
    public sealed class StringResourceFactory : IStringResourceFactory
    {
        #region Constructors
        private StringResourceFactory()
        {
        }
        #endregion

        #region Methods
        public static IStringResourceFactory Create()
        {
            Contract.Ensures(Contract.Result<IStringResourceFactory>() != null);
            return new StringResourceFactory();
        }

        public IStringResource Create(
            Guid id, 
            Guid displayLanguageId,
            string value)
        {
            return StringResource.Create(
                id,
                displayLanguageId,
                value);
        }
        #endregion
    }
}
