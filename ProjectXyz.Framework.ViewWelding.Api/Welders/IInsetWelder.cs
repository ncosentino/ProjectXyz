namespace ProjectXyz.Framework.ViewWelding.Api.Welders
{
    public interface IInsetWelder : IViewWelder
    {
        void Weld(IInsetWeldOptions weldOptions);
    }
}