using ImitateDance.Scripts.Presentation.Presenter.Game.Core;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.Character
{
    public sealed class CpuPackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CpuPresenter>();
        }
    }
}