using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Presentation.View.Common;
using ImitateDance.Scripts.Presentation.View.Tutorial;
using UniRx;
using UniScreen.Container;
using UniScreen.Extension;
using UnityEngine;
using UnityEngine.Playables;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class TutorialPresenter : IInitializable, IDisposable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly TutorialView _tutorialView = default;
        private readonly CloseScreenButton _closeScreenButton = default;
        private readonly AudioView _audioView = default;

        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public TutorialPresenter(ScreenContainer screenContainer, TutorialView tutorialView,
            CloseScreenButton closeScreenButton, AudioView audioView)
        {
            _screenContainer = screenContainer;
            _tutorialView = tutorialView;
            _closeScreenButton = closeScreenButton;
            _audioView = audioView;
        }

        public async void Initialize()
        {
            _closeScreenButton.OnClickAsObservable()
                .Subscribe(_ => Close())
                .AddTo(_disposable);

            await _audioView.Load("Tutorial", _cancellation.Token);
            _audioView.Play();

            await _tutorialView.PlayAsync(_cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            Close();
        }

        private async void Close()
        {
            await _audioView.Load("MainBgm", _cancellation.Token);
            _audioView.Play();
            _screenContainer.Pop().Forget();
        }

        public void Dispose()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _disposable.Dispose();
        }
    }
}