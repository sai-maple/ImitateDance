using System.Threading;
using Cysharp.Threading.Tasks;
using UniScreen.View;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImitateDunce.Applications.Factory
{
    public sealed class ScreenFactory : UniScreen.Factory.ScreenFactory
    {
        public override async UniTask<ScreenView> CreateAsync(string screen, CancellationToken token)
        {
            // var asset = (ScreenView)await Resources.LoadAsync<ScreenView>(screen);
            var asset = await Addressables.LoadAssetAsync<GameObject>(screen);
            return asset.GetComponent<ScreenView>().Create();
        }
    }
}