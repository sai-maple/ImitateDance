using ImitateDunce.Domain.Entity;
using VContainer;
using VContainer.Unity;

namespace ImitateDunce.Applications.Installer.Core
{
    public sealed class GameInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PhaseEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}