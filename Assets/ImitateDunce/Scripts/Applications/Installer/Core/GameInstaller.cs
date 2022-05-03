using ImitateDunce.Domain.Entity.Game.Core;
using VContainer;
using VContainer.Unity;

namespace ImitateDunce.Applications.Installer.Core
{
    public sealed class GameInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PhaseEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<ScoreEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<TimeEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<SpeedEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<TurnPlayerEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}