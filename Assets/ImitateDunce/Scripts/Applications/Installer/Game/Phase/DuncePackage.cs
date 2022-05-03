using ImitateDunce.Presentation.Presenter.Game.Phase;
using VContainer;
using VContainer.Unity;

namespace ImitateDunce.Applications.Installer.Game.Phase
{
    public sealed class DuncePackage : LifetimeScope
    {
        // todo View
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<DuncePresenter>();
        }
    }
}