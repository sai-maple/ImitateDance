using System;
using System.Threading;
using ImitateDance.Scripts.Domain.UseCase.Game.Core;
using UniScreen.Container;
using UniScreen.Extension;
using UnityEngine.Playables;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Core
{
    public sealed class IntroPresenter : IInitializable, IDisposable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly TurnUseCase _turnUseCase = default;
        private readonly PlayableDirector _intro = default;

        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();

        public IntroPresenter(ScreenContainer screenContainer, TurnUseCase turnUseCase, PlayableDirector intro)
        {
            _screenContainer = screenContainer;
            _turnUseCase = turnUseCase;
            _intro = intro;
        }

        public async void Initialize()
        {
            // todo BGMのストップ
            // intro
            await _intro.PlayAsync(_cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;

            // ゲーム用のUI表示
            await _screenContainer.Push("Game", token: _cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;

            // todo ゲーム音楽再生
            _turnUseCase.GameStart();
        }

        public void Dispose()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
        }
    }
}