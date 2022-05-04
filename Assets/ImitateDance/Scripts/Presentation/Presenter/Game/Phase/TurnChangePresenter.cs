using System;
using System.Threading;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Domain.UseCase.Game.Core;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Phase
{
    public sealed class TurnChangePresenter : IInitializable, IDisposable
    {
        private readonly PhaseEntity _phaseEntity = default;
        private readonly TurnUseCase _turnUseCase = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        public TurnChangePresenter(PhaseEntity phaseEntity, TurnUseCase turnUseCase)
        {
            _phaseEntity = phaseEntity;
            _turnUseCase = turnUseCase;
        }

        public void Initialize()
        {
            _phaseEntity.OnChangeAsObservable()
                .Where(phase => phase == DancePhase.TurnChange)
                .Subscribe(_ =>
                {
                    // todo viewの状態変化, 歓声の再生
                    _turnUseCase.OnTurnChange(_cancellationToken.Token);
                })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
        }
    }
}