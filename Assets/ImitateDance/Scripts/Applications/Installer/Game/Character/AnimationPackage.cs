using ImitateDance.Scripts.Presentation.Presenter.Game.Character;
using ImitateDance.Scripts.Presentation.View.Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.Character
{
    [RequireComponent(typeof(CharacterAnimationView))]
    public sealed class AnimationPackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CharacterAnimationPresenter>();
            builder.RegisterComponent(GetComponent<CharacterAnimationView>());
        }
    }
}