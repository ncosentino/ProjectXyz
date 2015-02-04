using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Data.Core.Items.Materials
{
    public sealed class Material : IMaterial
    {
        #region Constructors
        private Material(string materialType, string name)
        {
            Contract.Requires<ArgumentNullException>(materialType != null);
            Contract.Requires<ArgumentException>(materialType != string.Empty);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name != string.Empty);

            this.MaterialType = materialType;
            this.Name = name;
        }
        #endregion

        #region Properties
        public string Name
        {
            get;
            private set;
        }

        public string MaterialType
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IMaterial Create(string materialType, string name)
        {
            Contract.Requires<ArgumentNullException>(materialType != null);
            Contract.Requires<ArgumentException>(materialType != string.Empty);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name != string.Empty);
            Contract.Ensures(Contract.Result<IMaterial>() != null);
            return new Material(materialType, name);
        }
        #endregion
    }
}