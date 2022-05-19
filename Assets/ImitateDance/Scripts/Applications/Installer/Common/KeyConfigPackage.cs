using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Presentation.Presenter.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    [RequireComponent(typeof(KeyConfigView))]
    public sealed class KeyConfigPackage : LifetimeScope
    {
        [SerializeField] private DanceDirection _direction = default;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<KeyConfigPresenter>().WithParameter("direction", _direction);
            builder.RegisterComponent(GetComponent<KeyConfigView>());
        }
    }
}