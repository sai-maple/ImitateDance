using ImitateDance.Scripts.Presentation.Presenter.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    [RequireComponent(typeof(NewScreenButton))]
    public sealed class LoadScenePackage : LifetimeScope
    {
        [SerializeField] private string _sceneName = default;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LoadScenePresenter>().WithParameter("sceneName", _sceneName);
            builder.RegisterComponent(GetComponent<NewScreenButton>());
        }
    }
}