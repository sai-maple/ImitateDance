using System.Threading;
using Cysharp.Threading.Tasks;
using UniScreen.View;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImitateDance.Scripts.Applications.Factory
{
    public sealed class ScreenFactory : UniScreen.Factory.ScreenFactory
    {
        public override async UniTask<ScreenView> CreateAsync(string screen, Transform content, CancellationToken token)
        {
            // var asset = (ScreenView)await Resources.LoadAsync<ScreenView>(screen);
            var asset = await Addressables.LoadAssetAsync<GameObject>(screen).WithCancellation(token);
            return token.IsCancellationRequested ? null : asset.GetComponent<ScreenView>().Create(content);
        }
    }
}