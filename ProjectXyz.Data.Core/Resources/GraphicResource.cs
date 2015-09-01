using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Resources;

namespace ProjectXyz.Data.Core.Resources
{
    public sealed class GraphicResource : IGraphicResource
    {
        #region Constructors
        private GraphicResource(
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
        public static IGraphicResource Create(
            Guid id,
            Guid displayLanguageId,
            string value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(displayLanguageId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Ensures(Contract.Result<IGraphicResource>() != null);
            
            return new GraphicResource(
                id,
                displayLanguageId,
                value);
        }
        #endregion
    }
}
