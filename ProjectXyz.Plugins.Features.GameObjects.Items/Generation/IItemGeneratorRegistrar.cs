namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public interface IItemGeneratorRegistrar
    {
        void Register(IItemGenerator itemGenerator);
    }
}