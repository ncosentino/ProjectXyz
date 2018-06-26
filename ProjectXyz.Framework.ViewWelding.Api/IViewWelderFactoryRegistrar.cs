namespace ProjectXyz.Framework.ViewWelding.Api
{
    public interface IViewWelderFactoryRegistrar
    {
        void Register(CanWeldDelegate canWeldCallback, CreateWelderDelegate createWelderCallback);
    }
}