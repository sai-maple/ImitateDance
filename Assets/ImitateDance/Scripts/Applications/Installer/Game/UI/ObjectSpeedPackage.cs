using ImitateDance.Scripts.Presentation.Presenter.Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Game.UI
{
    [RequireComponent(typeof(Animator))]
    public sealed class ObjectSpeedPackage : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ObjectSpeedPresenter>().WithParameter("animator", GetComponent<Animator>());
        }
    }
}