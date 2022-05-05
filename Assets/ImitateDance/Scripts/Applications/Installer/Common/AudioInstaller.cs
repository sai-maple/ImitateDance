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
            builder.RegisterComponent(GetComponent<AudioView>());
        }
    }
}