using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class Requirements : IMutableRequirements
    {
        #region Fields
        private readonly ProjectXyz.Data.Interface.Items.IRequirements _requirementsData;
        #endregion

        #region Constructors
        private Requirements(ProjectXyz.Data.Interface.Items.IRequirements requirementsData)
        {
            Contract.Requires<ArgumentNullException>(requirementsData != null);
            _requirementsData = requirementsData;
        }
        #endregion
        
        #region Properties
        public int Level
        {
            get { return _requirementsData.Level; }
            set { _requirementsData.Level = value; }
        }

        public string Race
        {
            get { return _requirementsData.Race; }
            set { _requirementsData.Race = value; }
        }

        public string Class
        {
            get { return _requirementsData.Class; }
            set { _requirementsData.Class = value; }
        }

        IStatCollection IRequirements.Stats
        {
            get { return _requirementsData.Stats; }
        }

        IMutableStatCollection IMutableRequirements.Stats
        {
            get { return _requirementsData.Stats; }
        }
        #endregion

        #region Methods
        public static IMutableRequirements Create(ProjectXyz.Data.Interface.Items.IRequirements requirementsData)
        {
            Contract.Requires<ArgumentNullException>(requirementsData != null);
            Contract.Ensures(Contract.Result<IMutableRequirements>() != null);
            return new Requirements(requirementsData);
        }
        #endregion
    }
}
