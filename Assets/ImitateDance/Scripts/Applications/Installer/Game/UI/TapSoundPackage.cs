using ImitateDance.Scripts.Presentation.Presenter.Game.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.UI
{
    public sealed class TapSoundPackage : LifetimeScope
    {
        [SerializeField] private AudioSource _audioSource = default;
        [SerializeField] private AudioClip _audioClip = default;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TapSoundPresenter>()
                .WithParameter("audioSource", _audioSource)
                .WithParameter("audioClip", _audioClip);
        }
    }
}