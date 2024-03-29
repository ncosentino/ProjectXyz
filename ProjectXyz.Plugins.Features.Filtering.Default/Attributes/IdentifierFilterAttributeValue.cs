﻿using System;

using NexusLabs.Contracts;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Attributes
{
    public sealed class IdentifierFilterAttributeValue : IFilterAttributeValue
    {
        private readonly Lazy<IIdentifier> _lazyValue;

        public IdentifierFilterAttributeValue(IIdentifier value)
            : this(() => value)
        {
            Contract.RequiresNotNull(
                value,
                () => $"{nameof(value)} cannot be null.");
        }

        public IdentifierFilterAttributeValue(Func<IIdentifier> callback)
        {
            _lazyValue = new Lazy<IIdentifier>(() =>
            {
                var id = callback();
                Contract.RequiresNotNull(
                    id,
                    () => $"Identifier cannot be null.");
                return id;
            });
        }

        public IIdentifier Value => _lazyValue.Value;

        public override string ToString() =>
            $"ID: {Value}";
    }
}