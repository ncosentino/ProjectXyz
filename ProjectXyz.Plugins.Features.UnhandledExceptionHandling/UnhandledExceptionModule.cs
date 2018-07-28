using System;
using System.Threading.Tasks;
using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.UnhandledExceptionHandling
{
    public sealed class UnhandledExceptionModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<UnhandledErrorReporter>()
                .AsSelf()
                .AutoActivate()
                .OnActivated(x =>
                {
                    var reporter = x.Instance;
                    TaskScheduler.UnobservedTaskException += (_, e) =>
                    {
                        reporter.Report(e.Exception);
                        e.SetObserved();
                    };
                    AppDomain.CurrentDomain.UnhandledException += (_, e) => reporter.Report(e.ExceptionObject as Exception);
                });
        }
    }
}
