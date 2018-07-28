﻿using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Enchantments
{
    public sealed class EnchantmentsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new ValueMapperRepository(c.Resolve<TestData>()))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}