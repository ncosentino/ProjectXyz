using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Resources;

namespace ProjectXyz.Data.Core.Resources
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
