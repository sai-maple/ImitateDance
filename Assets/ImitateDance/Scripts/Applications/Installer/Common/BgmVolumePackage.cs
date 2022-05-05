using ImitateDance.Scripts.Presentation.Presenter.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class BgmVolumePackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BgmVolumePresenter>().WithParameter("audioSource", GetComponent<AudioSource>());
        }
    }
}