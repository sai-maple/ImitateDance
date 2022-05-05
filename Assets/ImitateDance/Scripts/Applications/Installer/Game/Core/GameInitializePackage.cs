using ImitateDance.Scripts.Presentation.Presenter.Game.Core;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.Core
{
    public sealed class GameInitializePackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameInitializePresenter>();
        }
    }
}