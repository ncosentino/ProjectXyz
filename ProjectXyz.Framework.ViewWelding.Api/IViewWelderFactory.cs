namespace ProjectXyz.Framework.ViewWelding.Api
{
    public interface IViewWelderFactory
    {
        TViewWelder Create<TViewWelder>(object parent, object child) where TViewWelder : IViewWelder;
    }
}