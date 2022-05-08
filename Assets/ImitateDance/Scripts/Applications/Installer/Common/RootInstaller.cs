using ImitateDance.Scripts.Domain.Entity.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    public sealed class RootInstaller : LifetimeScope
    {
        [SerializeField] private InstallerBase[] _installers = default;

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var installer in _installers)
            {
                installer.Configure(builder);
            }

            builder.Register<DifficultyEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<VolumeEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<HideButtonEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}