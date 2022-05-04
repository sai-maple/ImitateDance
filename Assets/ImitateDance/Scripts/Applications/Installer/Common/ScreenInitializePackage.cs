using ImitateDance.Scripts.Presentation.Presenter.Screen;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    public sealed class ScreenInitializePackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ScreenInitializePresenter>();
        }
    }
}