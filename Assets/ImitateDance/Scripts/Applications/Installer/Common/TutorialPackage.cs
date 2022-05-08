using ImitateDance.Scripts.Presentation.Presenter.Game.UI;
using UnityEngine;
using UnityEngine.Playables;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    public sealed class TutorialPackage : LifetimeScope
    {
        [SerializeField] private PlayableDirector _director = default;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TutorialPresenter>().WithParameter("tutorialDirector", _director);
        }
    }
}