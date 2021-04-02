namespace ProjectXyz.Framework.ViewWelding.Api.Welders
{
    public interface ISimpleWelder : IViewWelder
    {
        IWeldResult Weld();
    }
}