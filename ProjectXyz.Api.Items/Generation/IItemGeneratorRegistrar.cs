namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation
{
    public interface IItemGeneratorRegistrar
    {
        void Register(IItemGenerator itemGenerator);
    }
}