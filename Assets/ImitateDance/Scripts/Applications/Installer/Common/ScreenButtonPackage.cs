using ImitateDance.Scripts.Presentation.Presenter.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    [RequireComponent(typeof(ScreenButton))]
    public sealed class ScreenButtonPackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ScreenButtonPresenter>();
            builder.RegisterComponent(GetComponent<ScreenButton>());
        }
    }
}