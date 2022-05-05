using ImitateDance.Scripts.Presentation.Presenter.Game.Phase;
using UnityEngine;
using UnityEngine.Playables;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.Phase
{
    public sealed class IntroPackage : LifetimeScope
    {
        [SerializeField] private PlayableDirector _intro = default;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<IntroPresenter>().WithParameter("intro", _intro);
        }
    }
}