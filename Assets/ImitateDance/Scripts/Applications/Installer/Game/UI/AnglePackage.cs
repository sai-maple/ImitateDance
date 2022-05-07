using ImitateDance.Scripts.Presentation.Presenter.Game.UI;
using ImitateDance.Scripts.Presentation.View.Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.UI
{
    [RequireComponent(typeof(AngleView))]
    public sealed class AnglePackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<AnglePresenter>();
            builder.RegisterComponent(GetComponent<AngleView>());
        }
    }
}