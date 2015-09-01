using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Resources;

namespace ProjectXyz.Data.Core.Resources
{
    public sealed class GraphicResourceFactory : IGraphicResourceFactory
    {
        #region Constructors
        private GraphicResourceFactory()
        {
        }
        #endregion

        #region Methods
        public static IGraphicResourceFactory Create()
        {
            Contract.Ensures(Contract.Result<IGraphicResourceFactory>() != null);
            return new GraphicResourceFactory();
        }

        public IGraphicResource Create(
            Guid id, 
            Guid displayLanguageId,
            string value)
        {
            return GraphicResource.Create(
                id,
                displayLanguageId,
                value);
        }
        #endregion
    }
}
