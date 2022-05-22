using ImitateDance.Scripts.Presentation.Presenter.Game.UI;
using ImitateDance.Scripts.Presentation.View.Common;
using ImitateDance.Scripts.Presentation.View.Tutorial;
using UnityEngine;
using UnityEngine.Playables;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    [RequireComponent(typeof(TutorialView))]
    public sealed class TutorialPackage : LifetimeScope
    {
        [SerializeField] private CloseScreenButton _closeScreenButton = default;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TutorialPresenter>();
            builder.RegisterComponent(GetComponent<TutorialView>());
            builder.RegisterComponent(_closeScreenButton);
        }
    }
}