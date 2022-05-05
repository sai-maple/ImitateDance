using ImitateDance.Scripts.Applications.Factory;
using ImitateDance.Scripts.Presentation.Presenter.Screen;
using UniScreen.Container;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    public sealed class ScreenInitializePackage : InstallerBase
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<ScreenContainer>(Lifetime.Singleton)
                .WithParameter("factory", new ScreenFactory())
                .WithParameter(transform);

            builder.RegisterEntryPoint<ScreenInitializePresenter>();
        }
    }
}