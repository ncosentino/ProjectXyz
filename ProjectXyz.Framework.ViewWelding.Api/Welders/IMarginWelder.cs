namespace ProjectXyz.Framework.ViewWelding.Api.Welders
{
    public interface IMarginWelder : IViewWelder
    {
        void Weld(IMarginWeldOptions weldOptions);
    }
}