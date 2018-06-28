using System.Linq;
using Autofac;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<AttributeValueMatchFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    var attributeValueMatchFacade = x.Instance;
                    attributeValueMatchFacade.Register<
                        StringGeneratorAttributeValue,
                        StringCollectionGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2
                                .Values
                                .Contains(v1.Value);
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        StringGeneratorAttributeValue,
                        StringGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2.Value.Equals(v1.Value);
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        RangeGeneratorAttributeValue,
                        DoubleGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch =
                                v1.Minimum <= v2.Value &&
                                v1.Maximum >= v2.Value;
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        DoubleGeneratorAttributeValue,
                        RangeGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch =
                                v2.Minimum <= v1.Value &&
                                v2.Maximum >= v1.Value;
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        DoubleGeneratorAttributeValue,
                        DoubleGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v1.Value == v2.Value;
                            return isAttrtMatch;
                        });
                });
        }
    }
}
