using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class Requirements : IRequirements
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
        }

        public string Race
        {
            get { return _requirementsData.Race; }
        }

        public string Class
        {
            get { return _requirementsData.Class; }
        }

        public IStatCollection<IStat> Stats
        {
            get { return _requirementsData.Stats; }
        }
        #endregion

        #region Methods
        public static IRequirements Create(ProjectXyz.Data.Interface.Items.IRequirements requirementsData)
        {
            Contract.Requires<ArgumentNullException>(requirementsData != null);
            Contract.Ensures(Contract.Result<IRequirements>() != null);
            return new Requirements(requirementsData);
        }
        #endregion       
    }
}
