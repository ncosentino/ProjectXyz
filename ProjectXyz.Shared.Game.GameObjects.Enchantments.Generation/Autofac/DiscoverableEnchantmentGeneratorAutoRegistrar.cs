using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Logging;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.Autofac
{
    public sealed class DiscoverableEnchantmentGeneratorAutoRegistrar
    {
        public DiscoverableEnchantmentGeneratorAutoRegistrar(
            IEnumerable<IEnchantmentGeneratorFacade> facades,
            IEnumerable<IDiscoverableEnchantmentGenerator> discoverableEnchantmentGenerators,
            ILogger logger)
        {
            foreach (var facade in facades)
            {
                logger.Debug($"Registering enchantment generators to '{facade}'...");
                foreach (var generator in discoverableEnchantmentGenerators)
                {
                    logger.Debug($"Registering '{generator}' to '{facade}'.");
                    facade.Register(generator);
                }

                logger.Debug($"Done registering enchantment generators to '{facade}'.");
            }
        }
    }
}