using ImitateDance.Scripts.Domain.UseCase.Game.Core;
using ImitateDance.Scripts.Presentation.Presenter.Game.UI;
using ImitateDance.Scripts.Presentation.View.Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.UI
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