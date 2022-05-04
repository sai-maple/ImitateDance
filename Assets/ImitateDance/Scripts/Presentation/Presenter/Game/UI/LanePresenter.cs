using System;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Domain.UseCase.Game.NoteLine;
using ImitateDance.Scripts.Presentation.View.Game;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class LanePresenter : IInitializable, IDisposable
    {
        private readonly ScoreEntity _scoreEntity = default;
        private readonly PhaseEntity _phaseEntity = default;
        private readonly LanePositionUseCase _lanePositionUseCase = default;
        private readonly LanePairView _lanePairView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public LanePresenter(ScoreEntity scoreEntity, PhaseEntity phaseEntity, LanePositionUseCase lanePositionUseCase,
            LanePairView lanePairView)
        {
            _scoreEntity = scoreEntity;
            _phaseEntity = phaseEntity;
            _lanePositionUseCase = lanePositionUseCase;
            _lanePairView = lanePairView;
        }

        public void Initialize()
        {
            _scoreEntity.OnScoreAsObservable()
                .Subscribe(_lanePairView.Initialize)
                .AddTo(_disposable);

            _phaseEntity.OnChangeAsObservable()
                .Where(phase => phase == DancePhase.TurnChange)
                .Subscribe(_ => _lanePairView.HideDemo())
                .AddTo(_disposable);

            Observable.EveryFixedUpdate()
                .Subscribe(_ => _lanePairView.Play(_phaseEntity.Current, _lanePositionUseCase.NormalizePosition()))
                .AddTo(_disposable);

            _scoreEntity.OnDanceAsObservable()
                .Subscribe(dunce => _lanePairView.OnDance(_phaseEntity.Current, dunce))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}