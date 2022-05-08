using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using UniScreen.Container;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class GameStartButtonPresenter : IInitializable, IDisposable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly DifficultyEntity _difficultyEntity = default;
        private readonly DifficultyButton _difficultyButton = default;
        private readonly AudioView _audioView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();

        public GameStartButtonPresenter(ScreenContainer screenContainer, DifficultyEntity difficultyEntity,
            DifficultyButton difficultyButton, AudioView audioView)
        {
            _screenContainer = screenContainer;
            _difficultyEntity = difficultyEntity;
            _difficultyButton = difficultyButton;
            _audioView = audioView;
        }

        public void Initialize()
        {
            _difficultyButton.OnClickAsObservable()
                .Subscribe(difficulty =>
                {
                    _difficultyEntity.Set(difficulty);
                    LoadSceneAsync("GameScreen", "Loading");
                }).AddTo(_disposable);
        }

        private async void LoadSceneAsync(string canvasName, string screenName)
        {
            _audioView.StopAsync().Forget();
            await _screenContainer.NewScreen(canvasName, token: _cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            await _screenContainer.Push(screenName, token: _cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            await SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}