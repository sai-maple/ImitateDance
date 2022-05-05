using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Domain.UseCase.Game.Core;
using UniRx;
using UniScreen.Container;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Core
{
    public sealed class PhasePresenter : IInitializable, IDisposable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly MusicEntity _musicEntity = default;
        private readonly PhaseEntity _phaseEntity = default;
        private readonly TurnUseCase _turnUseCase = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        public PhasePresenter(ScreenContainer screenContainer, MusicEntity musicEntity, PhaseEntity phaseEntity,
            TurnUseCase turnUseCase)
        {
            _screenContainer = screenContainer;
            _musicEntity = musicEntity;
            _phaseEntity = phaseEntity;
            _turnUseCase = turnUseCase;
        }

        public void Initialize()
        {
            _phaseEntity.OnChangeAsObservable()
                .Where(phase => phase == DancePhase.Audience)
                .Subscribe(_ => _turnUseCase.OnAudience(_cancellationToken.Token))
                .AddTo(_disposable);

            _phaseEntity.OnChangeAsObservable()
                .Where(phase => phase == DancePhase.Dance)
                .Subscribe(_ => OnDance())
                .AddTo(_disposable);

            _phaseEntity.OnChangeAsObservable()
                .Where(phase => phase == DancePhase.Demo)
                .Subscribe(_ => OnDemo())
                .AddTo(_disposable);

            _phaseEntity.OnChangeAsObservable()
                .Where(phase => phase == DancePhase.TurnChange)
                .Subscribe(_ => _turnUseCase.OnTurnChange(_cancellationToken.Token))
                .AddTo(_disposable);

            _musicEntity.OnFinishAsObservable()
                .Take(1)
                .Subscribe(_ => _screenContainer.Push("Result").Forget());
        }

        private async void OnDance()
        {
            await _turnUseCase.OnDance(_cancellationToken.Token);
        }

        private async void OnDemo()
        {
            await _turnUseCase.OnDemo(_cancellationToken.Token);
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
        }
    }
}