using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Presentation.View.Common;
using ImitateDance.Scripts.Presentation.View.Game;
using UniRx;
using UniScreen.Container;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class ResultPresenter : IInitializable, IDisposable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly PointEntity _pointEntity = default;
        private readonly ResultView _resultView = default;
        private readonly AudioView _audioView = default;

        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public ResultPresenter(ScreenContainer screenContainer, PointEntity pointEntity, ResultView resultView,
            AudioView audioView)
        {
            _screenContainer = screenContainer;
            _pointEntity = pointEntity;
            _resultView = resultView;
            _audioView = audioView;
        }

        public async void Initialize()
        {
            PlayAudio(_cancellation.Token);
            _resultView.OnCloseAsObservable()
                .Subscribe(_ => CloseAsync())
                .AddTo(_disposable);

            await _resultView.PlayAsync(_pointEntity.Winner(), _cancellation.Token);
        }

        private async void PlayAudio(CancellationToken token)
        {
            await _audioView.Load("Result", token);
            _audioView.Play();
        }

        private async void CloseAsync()
        {
            await SceneManager.UnloadSceneAsync("GameScene").WithCancellation(_cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            _screenContainer.Close().Forget();
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}