using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Resources.Interface.Strings;

namespace ProjectXyz.Data.Resources.Core.Strings
{
    public sealed class StringResource : IStringResource
    {
        #region Constructors
        private StringResource(
            Guid id,
            Guid displayLanguageId,
            string value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(displayLanguageId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(value != null);

            Id = id;
            DisplayLanguageId = displayLanguageId;
            Value = value;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get;
            private set;
        }
        
        public Guid DisplayLanguageId
        {
            get;
            private set;
        }

        public string Value
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IStringResource Create(
            Guid id,
            Guid displayLanguageId,
            string value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(displayLanguageId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Ensures(Contract.Result<IStringResource>() != null);
            
            return new StringResource(
                id,
                displayLanguageId,
                value);
        }
        #endregion
    }
}
