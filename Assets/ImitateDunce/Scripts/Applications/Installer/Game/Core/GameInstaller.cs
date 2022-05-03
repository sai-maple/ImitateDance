using ImitateDunce.Domain.Entity.Game.Core;
using ImitateDunce.Domain.UseCase.Game.Core;
using ImitateDunce.Presentation.Presenter.Game.Core;
using VContainer;
using VContainer.Unity;

namespace ImitateDunce.Applications.Installer.Game.Core
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
            builder.Register<MusicEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<PointEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<TurnUseCase>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<TimePresenter>();
        }
    }
}