using System;
using ImitateDunce.Applications.Enums;
using ImitateDunce.Domain.Entity.Game.Core;
using ImitateDunce.Domain.UseCase.Game.NoteLine;
using ImitateDunce.Presentation.View;
using ImitateDunce.Presentation.View.Game;
using UniRx;
using VContainer.Unity;

namespace ImitateDunce.Presentation.Presenter.Game.UI
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
                .Where(phase => phase == DuncePhase.TurnChange)
                .Subscribe(_ => _lanePairView.HideDemo())
                .AddTo(_disposable);

            Observable.EveryFixedUpdate()
                .Subscribe(_ => _lanePairView.Play(_phaseEntity.Current, _lanePositionUseCase.NormalizePosition()))
                .AddTo(_disposable);

            _scoreEntity.OnDunceAsObservable()
                .Subscribe(dunce => _lanePairView.OnDunce(_phaseEntity.Current, dunce))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}