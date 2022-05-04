using ImitateDunce.Domain.UseCase.Game.Core;
using ImitateDunce.Presentation.Presenter.Game.UI;
using ImitateDunce.Presentation.View.Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDunce.Applications.Installer.Game.UI
{
    [RequireComponent(typeof(InputView))]
    public sealed class TapPackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputUseCase>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<TapPresenter>();
            builder.RegisterComponent(GetComponent<InputView>());
        }
    }
}