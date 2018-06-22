namespace ProjectXyz.Api.GameObjects.Items.Generation
{
    public interface IItemGeneratorRegistrar
    {
        void Register(IItemGenerator itemGenerator);
    }
}