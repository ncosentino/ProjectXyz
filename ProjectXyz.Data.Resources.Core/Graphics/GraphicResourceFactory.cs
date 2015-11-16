using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Resources.Interface.Graphics;

namespace ProjectXyz.Data.Resources.Core.Graphics
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
