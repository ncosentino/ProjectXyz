using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Interface.Diseases;

namespace ProjectXyz.Data.Core.Diseases
{
    public sealed class DiseaseSpreadType : IDiseaseSpreadType
    {
        #region Fields
        private readonly Guid _id;
        private readonly string _name;
        #endregion

        #region Constructors
        private DiseaseSpreadType(
            Guid id,
            string name)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name != string.Empty);

            _id = id;
            _name = name;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }
        #endregion

        #region Methods
        public static IDiseaseSpreadType Create(
            Guid id,
            string name)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name != string.Empty);
            Contract.Ensures(Contract.Result<IDiseaseSpreadType>() != null);

            return new DiseaseSpreadType(
                id,
                name);
        }
        #endregion
    }
}
