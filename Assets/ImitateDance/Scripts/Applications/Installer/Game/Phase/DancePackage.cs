using ImitateDance.Scripts.Presentation.Presenter.Game.Phase;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.Phase
{
    public sealed class DancePackage : LifetimeScope
    {
        // todo View
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<DancePresenter>();
        }
    }
}