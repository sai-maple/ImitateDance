using ImitateDance.Scripts.Presentation.Presenter.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    [RequireComponent(typeof(ScreenButton))]
    public sealed class BackScreenPackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BackScreenPresenter>();
            builder.RegisterComponent(GetComponent<ScreenButton>());
        }
    }
}