using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Presentation.Presenter.Game.UI;
using ImitateDance.Scripts.Presentation.View.Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.UI
{
    [RequireComponent(typeof(InputView))]
    public sealed class TapPackage : LifetimeScope
    {
        [SerializeField] private DanceDirection _direction = default;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TapPresenter>().WithParameter("direction", _direction);
            builder.RegisterComponent(GetComponent<InputView>());
        }
    }
}