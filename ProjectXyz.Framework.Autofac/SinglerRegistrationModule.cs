using System;
using Autofac;

namespace ProjectXyz.Framework.Autofac
{
    public abstract class SingleRegistrationModule : Module
    {
        private static readonly string PREFIX = $"{Guid.NewGuid()}_RegistrationCount_";

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var propertyKey = $"{PREFIX}{GetType().FullName}";
            if (builder.Properties.ContainsKey(propertyKey))
            {
                throw new InvalidOperationException(
                    $"Single registration module '{GetType()}' has already been registered.");
            }

            builder.Properties[propertyKey] = new object();
        }
    }
}
