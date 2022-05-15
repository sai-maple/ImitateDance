using System;
using ImitateDance.Scripts.Applications.Data;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Presentation.View.Game;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Character
{
    public sealed class CharacterAnimationPresenter : IInitializable
    {
        private readonly ScoreEntity _scoreEntity = default;
        private readonly TurnPlayerEntity _turnPlayerEntity = default;
        private readonly PhaseEntity _phaseEntity = default;
        private readonly SpeedEntity _speedEntity = default;
        private readonly PointEntity _pointEntity = default;
        private readonly MusicEntity _musicEntity = default;
        private readonly CharacterAnimationView _animationView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public CharacterAnimationPresenter(ScoreEntity scoreEntity, TurnPlayerEntity turnPlayerEntity,
            PhaseEntity phaseEntity, SpeedEntity speedEntity, PointEntity pointEntity, MusicEntity musicEntity,
            CharacterAnimationView animationView)
        {
            _scoreEntity = scoreEntity;
            _turnPlayerEntity = turnPlayerEntity;
            _phaseEntity = phaseEntity;
            _speedEntity = speedEntity;
            _pointEntity = pointEntity;
            _musicEntity = musicEntity;
            _animationView = animationView;
        }

        public void Initialize()
        {
            _scoreEntity.OnDanceAsObservable()
                .Subscribe(OnDance)
                .AddTo(_disposable);

            _speedEntity.OnChangeAsObservable()
                .Subscribe(_animationView.SetSpeed)
                .AddTo(_disposable);

            _pointEntity.OnWinnerAsObservable()
                .Subscribe(_animationView.Result)
                .AddTo(_disposable);

            _musicEntity.OnFinishAsObservable()
                .Subscribe(_ => _animationView.Finish())
                .AddTo(_disposable);
        }

        private void OnDance(DanceData danceData)
        {
            switch (_phaseEntity.Current)
            {
                case DancePhase.Dance:
                    _animationView.Dance(danceData.Dance, _turnPlayerEntity.Current);
                    break;
                case DancePhase.Demo:
                    _animationView.Dance(danceData.Demo, _turnPlayerEntity.Current);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}