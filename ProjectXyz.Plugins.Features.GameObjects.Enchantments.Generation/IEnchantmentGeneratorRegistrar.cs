namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation
{
    public interface IEnchantmentGeneratorRegistrar
    {
        void Register(IEnchantmentGenerator enchantmentGenerator);
    }
}