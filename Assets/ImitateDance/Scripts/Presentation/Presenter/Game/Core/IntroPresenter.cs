using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Domain.UseCase.Game.Core;
using ImitateDance.Scripts.Presentation.View.Common;
using UniScreen.Container;
using UniScreen.Extension;
using UnityEngine.Playables;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Core
{
    public sealed class IntroPresenter : IInitializable, IDisposable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly DifficultyEntity _difficultyEntity = default;
        private readonly TurnUseCase _turnUseCase = default;
        private readonly PlayableDirector _intro = default;
        private readonly AudioView _audioView = default;

        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();

        public IntroPresenter(ScreenContainer screenContainer, DifficultyEntity difficultyEntity,
            TurnUseCase turnUseCase, PlayableDirector intro, AudioView audioView)
        {
            _screenContainer = screenContainer;
            _difficultyEntity = difficultyEntity;
            _turnUseCase = turnUseCase;
            _intro = intro;
            _audioView = audioView;
        }

        public async void Initialize()
        {
            await _audioView.Load("Intro", _cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;

            // 2.13でライトアップ完了 1.5で最後のライトがつく
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: _cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;

            _audioView.Play();

            // intro
            await _intro.PlayAsync(_cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;

            // ゲーム用のUI表示
            await _screenContainer.Push("Game", token: _cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;

            await _audioView.Load($"GameMusic{_difficultyEntity.Value}", _cancellation.Token);

            await _audioView.StopAsync(_cancellation.Token);
            _audioView.PlayGameMusic();
            _turnUseCase.GameStart();
        }

        public void Dispose()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
        }
    }
}