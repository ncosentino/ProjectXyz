using System.Linq;

using Autofac;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default;

namespace ProjectXyz.Shared.Behaviors.Filtering.Autofac
{
    public sealed class FilteringModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<InMemoryAttributeFilterer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<FilterContextProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<FilterContextFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AttributeValueMatchFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    var attributeValueMatchFacade = x.Instance;
                    attributeValueMatchFacade.Register(
                        (v1, v2) => v1 is TrueAttributeFilterValue || v2 is TrueAttributeFilterValue,
                        (v1, v2) => true);
                    attributeValueMatchFacade.Register(
                        (v1, v2) => v1 is FalseAttributeFilterValue || v2 is FalseAttributeFilterValue,
                        (v1, v2) => false);
                    attributeValueMatchFacade.Register<
                        NotFilterAttributeValue,
                        IFilterAttributeValue>(
                        (v1, v2) =>
                        {
                            return !attributeValueMatchFacade.Match(v1.Wrapped, v2);
                        });
                    attributeValueMatchFacade.Register<
                        StringFilterAttributeValue,
                        AnyStringCollectionFilterAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2
                                .Values
                                .Contains(v1.Value);
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        StringFilterAttributeValue,
                        AllStringCollectionFilterAttributeValue>(
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
                        AnyStringCollectionFilterAttributeValue,
                        AnyStringCollectionFilterAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2
                                .Values
                                .Any(attr => v1.Values.Contains(attr));
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        AllStringCollectionFilterAttributeValue,
                        AllStringCollectionFilterAttributeValue>(
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
                        AllStringCollectionFilterAttributeValue,
                        AnyStringCollectionFilterAttributeValue>(
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
                        StringFilterAttributeValue,
                        StringFilterAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2.Value.Equals(v1.Value);
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        RangeFilterAttributeValue,
                        DoubleFilterAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch =
                                v1.Minimum <= v2.Value &&
                                v1.Maximum >= v2.Value;
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        DoubleFilterAttributeValue,
                        DoubleFilterAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v1.Value == v2.Value;
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        BooleanFilterAttributeValue,
                        BooleanFilterAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v1.Value == v2.Value;
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        IdentifierFilterAttributeValue,
                        IdentifierFilterAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v1.Value.Equals(v2.Value);
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        IdentifierFilterAttributeValue,
                        AnyIdentifierCollectionFilterAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2
                                .Values
                                .Contains(v1.Value);
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        IdentifierFilterAttributeValue,
                        AllIdentifierCollectionFilterAttributeValue>(
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
                        AnyIdentifierCollectionFilterAttributeValue,
                        AnyIdentifierCollectionFilterAttributeValue>(
                        (v1, v2) =>
                        {
                            var isAttrtMatch = v2
                                .Values
                                .Any(attr => v1.Values.Contains(attr));
                            return isAttrtMatch;
                        });
                    attributeValueMatchFacade.Register<
                        AllIdentifierCollectionFilterAttributeValue,
                        AllIdentifierCollectionFilterAttributeValue>(
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
                        AllIdentifierCollectionFilterAttributeValue,
                        AnyIdentifierCollectionFilterAttributeValue>(
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
                });
        }
    }
}
