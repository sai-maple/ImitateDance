using ImitateDance.Scripts.Domain.Entity.Game.NoteLine;
using ImitateDance.Scripts.Domain.UseCase.Game.NoteLine;
using ImitateDance.Scripts.Presentation.Presenter.Game.UI;
using ImitateDance.Scripts.Presentation.View.Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.UI
{
    [RequireComponent(typeof(LanePairView))]
    public sealed class NoteLinePackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PositionEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<LanePositionUseCase>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            // todo presenter and view
            builder.RegisterEntryPoint<LanePresenter>();
            builder.RegisterComponent(GetComponent<LanePairView>());
        }
    }
}