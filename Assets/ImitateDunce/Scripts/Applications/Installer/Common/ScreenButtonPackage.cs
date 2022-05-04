using ImitateDunce.Presentation.Presenter.Common;
using ImitateDunce.Presentation.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDunce.Applications.Installer.Common
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