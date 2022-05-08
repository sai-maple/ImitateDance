using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniScreen.Container;
using UniScreen.Extension;
using UnityEngine.Playables;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class TutorialPresenter : IInitializable, IDisposable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly PlayableDirector _tutorialDirector = default;

        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();

        public TutorialPresenter(ScreenContainer screenContainer, PlayableDirector tutorialDirector)
        {
            _screenContainer = screenContainer;
            _tutorialDirector = tutorialDirector;
        }

        public async void Initialize()
        {
            await _tutorialDirector.PlayAsync(_cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            _screenContainer.Pop().Forget();
        }


        public void Dispose()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
        }
    }
}