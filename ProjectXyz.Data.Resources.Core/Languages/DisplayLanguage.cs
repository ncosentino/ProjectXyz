using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Resources.Interface.Languages;

namespace ProjectXyz.Data.Resources.Core.Languages
{
    public sealed class DisplayLanguage : IDisplayLanguage
    {
        #region Constructors
        private DisplayLanguage(
            Guid id,
            string name)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));

            Id = id;
            Name = name;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get;
            private set;
        }
        
        public string Name
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IDisplayLanguage Create(
            Guid id,
            string name)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
            Contract.Ensures(Contract.Result<IDisplayLanguage>() != null);
            
            return new DisplayLanguage(
                id,
                name);
        }
        #endregion
    }
}
