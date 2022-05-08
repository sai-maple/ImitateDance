using ImitateDance.Scripts.Presentation.Presenter.Game.UI;
using ImitateDance.Scripts.Presentation.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    [RequireComponent(typeof(HideButtonToggle))]
    public sealed class HideButtonPackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<HideButtonPresenter>();
            builder.RegisterComponent(GetComponent<HideButtonToggle>());
        }
    }
}