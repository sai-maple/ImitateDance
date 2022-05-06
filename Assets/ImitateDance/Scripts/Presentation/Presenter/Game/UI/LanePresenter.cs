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
        private readonly MusicEntity _musicEntity = default;
        private readonly TimeEntity _timeEntity = default;
        private readonly ScoreEntity _scoreEntity = default;
        private readonly PhaseEntity _phaseEntity = default;
        private readonly LanePositionUseCase _lanePositionUseCase = default;
        private readonly LanePairView _lanePairView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public LanePresenter(MusicEntity musicEntity, TimeEntity timeEntity, ScoreEntity scoreEntity,
            PhaseEntity phaseEntity, LanePositionUseCase lanePositionUseCase, LanePairView lanePairView)
        {
            _musicEntity = musicEntity;
            _timeEntity = timeEntity;
            _scoreEntity = scoreEntity;
            _phaseEntity = phaseEntity;
            _lanePositionUseCase = lanePositionUseCase;
            _lanePairView = lanePairView;
        }

        public void Initialize()
        {
            _lanePairView.Initialize(_musicEntity.Score);
            _scoreEntity.OnScoreAsObservable()
                .Subscribe(note => _lanePairView.UpdateNote(_phaseEntity.Current, note))
                .AddTo(_disposable);

            Observable.EveryFixedUpdate()
                .Subscribe(_ =>
                {
                    UpdateScore();
                    _lanePairView.Play(_phaseEntity.Current, _lanePositionUseCase.NormalizePosition());
                })
                .AddTo(_disposable);

            _scoreEntity.OnDanceAsObservable()
                .Subscribe(dunce => _lanePairView.OnDance(_phaseEntity.Current, dunce))
                .AddTo(_disposable);
        }

        private void UpdateScore()
        {
            switch (_phaseEntity.Current)
            {
                case DancePhase.Dance:
                    _scoreEntity.UpdateDemo(_timeEntity.Time, _musicEntity.HalfBarTime);
                    break;
                case DancePhase.Demo:
                    _scoreEntity.UpdateDunce(_timeEntity.Time, _musicEntity.HalfBarTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}