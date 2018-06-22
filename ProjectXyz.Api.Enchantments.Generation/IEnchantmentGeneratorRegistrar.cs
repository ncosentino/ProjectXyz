namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentGeneratorRegistrar
    {
        void Register(IEnchantmentGenerator enchantmentGenerator);
    }
}