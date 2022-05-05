using ImitateDance.Scripts.Presentation.Presenter.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    [RequireComponent(typeof(AudioView))]
    public sealed class AudioInstaller : InstallerBase
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BgmVolumePresenter>();
            builder.RegisterComponent(GetComponent<AudioView>());
        }
    }
}