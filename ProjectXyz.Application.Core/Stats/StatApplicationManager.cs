using System.Diagnostics.Contracts;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Application.Core.Stats
{
    public sealed class StatApplicationManager : IStatApplicationManager
    {
        #region Fields
        private readonly IStatGenerator _statGenerator;
        #endregion

        #region Constructors
        private StatApplicationManager(IDataManager dataManager)
        {
            _statGenerator = Stats.StatGenerator.Create(dataManager.Stats.StatFactory);
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IStatGenerator StatGenerator
        {
            get { return _statGenerator; }
        }
        #endregion

        #region Methods
        public static IStatApplicationManager Create(IDataManager dataManager)
        {
            Contract.Ensures(Contract.Result<IStatApplicationManager>() != null);

            return new StatApplicationManager(dataManager);
        }
        #endregion
    }
}
