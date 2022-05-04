using ImitateDance.Scripts.Applications.Factory;
using UniScreen.Container;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    public sealed class RootInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ScreenContainer>(Lifetime.Singleton)
                .WithParameter("factory", new ScreenFactory())
                .WithParameter(transform);
        }
    }
}