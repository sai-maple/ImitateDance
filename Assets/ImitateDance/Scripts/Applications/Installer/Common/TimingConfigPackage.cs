using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Presentation.Presenter.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    public sealed class TimingConfigPackage : LifetimeScope
    {
        [SerializeField] private TimingLineView _timingLineView = default;
        [SerializeField] private TimingConfigView _timingConfigView = default;
        [SerializeField] private CloseScreenButton _closeScreenButton = default;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TimingLineEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.RegisterEntryPoint<TimingConfigPresenter>();
            builder.RegisterEntryPoint<TimingLinePresenter>();
            builder.RegisterComponent(_timingConfigView);
            builder.RegisterComponent(_timingLineView);
            builder.RegisterComponent(_closeScreenButton);
        }
    }
}