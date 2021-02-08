using System.Collections.Generic;
using System.Linq;
using Autofac;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Logging;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<GeneratorComponentToBehaviorConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(c =>
                {
                    var logger = c.Context.Resolve<ILogger>();
                    var facade = c.Context.Resolve<IGeneratorComponentToBehaviorConverterFacade>();
                    logger.Debug($"Registering converters to '{facade}'...");

                    var discovered = c.Context.Resolve<IEnumerable<IDiscoverableGeneratorComponentToBehaviorConverter>>();
                    foreach (var converter in discovered)
                    {
                        logger.Debug($"Registering '{converter}' on type '{converter.ComponentType}' to '{facade}'.");
                        facade.Register(
                            converter.ComponentType,
                            converter.Convert);
                    }

                    logger.Debug($"Done registering converters to '{facade}'.");
                });
            builder
                .RegisterType<GeneratorContextProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<GeneratorContextFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AttributeValueMatchFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    var attributeValueMatchFacade = x.Instance;
                    attributeValueMatchFacade.Register<
                        NotGeneratorAttributeValue,
                        IGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            return !attributeValueMatchFacade.Match(v1.Wrapped, v2);
                        });
                    attributeValueMatchFacade.Register<
                        StringGeneratorAttributeValue,
                        AnyStringCollectionGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2
                                .Values
                                .Contains(v1.Value);
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        StringGeneratorAttributeValue,
                        AllStringCollectionGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            if (v2.Values.Count != 1)
                            {
                                return false;
                            }

                            var isAttrtMatch = v2
                                .Values
                                .Single()
                                .Equals(v1.Value);
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        AnyStringCollectionGeneratorAttributeValue,
                        AnyStringCollectionGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2
                                .Values
                                .Any(attr => v1.Values.Contains(attr));
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        AllStringCollectionGeneratorAttributeValue,
                        AllStringCollectionGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2
                                .Values
                                .All(attr => v1.Values.Contains(attr))
                                && v1
                                .Values
                                .All(attr => v2.Values.Contains(attr));
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        AllStringCollectionGeneratorAttributeValue,
                        AnyStringCollectionGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2
                                .Values
                                .All(attr => v1.Values.Contains(attr))
                                && v1
                                .Values
                                .All(attr => v2.Values.Contains(attr));
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
                        DoubleGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v1.Value == v2.Value;
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        IdentifierGeneratorAttributeValue,
                        IdentifierGeneratorAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v1.Value.Equals(v2.Value);
                            return isAttrtMatch;
                        });
                });
        }
    }
}
