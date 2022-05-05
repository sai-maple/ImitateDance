using UnityEngine;
using VContainer;

namespace ImitateDance.Scripts.Applications.Installer.Common
{
    public abstract class InstallerBase : MonoBehaviour
    {
        public virtual void Configure(IContainerBuilder builder)
        {
        }
    }
}