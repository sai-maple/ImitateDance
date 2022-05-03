using System;
using System.Threading;
using ImitateDunce.Applications.Enums;
using ImitateDunce.Domain.Entity.Game.Core;
using ImitateDunce.Domain.UseCase.Game.Core;
using UniRx;
using VContainer.Unity;

namespace ImitateDunce.Presentation.Presenter.Game.Phase
{
    public sealed class DuncePresenter : IInitializable, IDisposable
    {
        private readonly PhaseEntity _phaseEntity = default;
        private readonly TurnUseCase _turnUseCase = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        public DuncePresenter(PhaseEntity phaseEntity, TurnUseCase turnUseCase)
        {
            _phaseEntity = phaseEntity;
            _turnUseCase = turnUseCase;
        }

        public void Initialize()
        {
            _phaseEntity.OnChangeAsObservable()
                .Where(phase => phase == DuncePhase.Dunce)
                .Subscribe(_ =>
                {
                    OnDunce();
                })
                .AddTo(_disposable);
        }

        private async void OnDunce()
        {
            // todo viewの状態変化
            // demo の譜面を隠す
            await _turnUseCase.OnDunce(_cancellationToken.Token);
            // キャラの状態を元に戻す
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
        }
    }
}