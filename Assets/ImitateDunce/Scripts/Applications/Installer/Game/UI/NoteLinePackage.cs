using ImitateDunce.Domain.Entity.Game.NoteLine;
using ImitateDunce.Domain.UseCase.Game.NoteLine;
using ImitateDunce.Presentation.Presenter.Game.UI;
using ImitateDunce.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDunce.Applications.Installer.Game.UI
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