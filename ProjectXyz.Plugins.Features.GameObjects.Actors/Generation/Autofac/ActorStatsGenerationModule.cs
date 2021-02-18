using Autofac;

using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Generation.Autofac
{
    public sealed class ActorStatsGenerationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ActorStatGeneratorAttributeProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterBuildCallback(c =>
            {
                var attributeValueMatchFacade = c.Resolve<IAttributeValueMatchFacade>();
                attributeValueMatchFacade.Register<
                    ActorStatGeneratorAttributeValueProvider,
                    ActorStatGeneratorAttributeValue>(
                    (v1, v2) =>
                    {
                        if (!v1.TryGetActorStat(
                            v2.ActorId,
                            v2.StatDefinitionId,
                            out var statValue))
                        {
                            return false;
                        }

                        var match =
                            v2.Minimum <= statValue &&
                            statValue <= v2.Maximum;
                        return match;
                    });
            });
        }
    }
}