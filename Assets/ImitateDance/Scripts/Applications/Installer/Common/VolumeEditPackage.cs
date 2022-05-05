using ImitateDance.Scripts.Presentation.Presenter.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    [RequireComponent(typeof(VolumeSliderView))]
    public sealed class VolumeEditPackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<VolumeEditPresenter>();
            builder.RegisterComponent(GetComponent<VolumeSliderView>());
        }
    }
}