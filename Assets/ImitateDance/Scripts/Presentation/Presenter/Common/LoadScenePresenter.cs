using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using UniScreen.Container;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class LoadScenePresenter : IInitializable, IDisposable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly NewScreenButton _button = default;
        private readonly string _sceneName = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();

        public LoadScenePresenter(ScreenContainer screenContainer, NewScreenButton button, string sceneName)
        {
            _screenContainer = screenContainer;
            _button = button;
            _sceneName = sceneName;
        }

        public void Initialize()
        {
            _button.OnClickAsObservable()
                .Subscribe(tuple => LoadSceneAsync(tuple.Item1, tuple.Item2))
                .AddTo(_disposable);
        }

        private async void LoadSceneAsync(string canvasName, string screenName)
        {
            await _screenContainer.NewScreen(canvasName, token: _cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            await _screenContainer.Push(screenName, token: _cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            await SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}