using ImitateDance.Scripts.Presentation.Presenter.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    [RequireComponent(typeof(DifficultyButton))]
    public sealed class GameStartButtonPackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameStartButtonPresenter>();
            builder.RegisterComponent(GetComponent<DifficultyButton>());
        }
    }
}