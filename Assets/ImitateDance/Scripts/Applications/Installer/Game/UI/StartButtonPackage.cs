using ImitateDance.Scripts.Presentation.Presenter.Common;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.UI
{
    public sealed class StartButtonPackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<StartButtonPresenter>().WithParameter("button", GetComponent<Button>());
        }
    }
}