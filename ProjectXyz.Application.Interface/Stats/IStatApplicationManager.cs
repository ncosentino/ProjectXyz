namespace ProjectXyz.Application.Interface.Stats
{
    public interface IStatApplicationManager
    {
        #region Properties
        IStatGenerator StatGenerator { get; }
        #endregion
    }
}
