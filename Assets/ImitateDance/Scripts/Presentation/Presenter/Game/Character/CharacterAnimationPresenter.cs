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
        private readonly CharacterAnimationView _animationView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public CharacterAnimationPresenter(ScoreEntity scoreEntity, TurnPlayerEntity turnPlayerEntity,
            PhaseEntity phaseEntity, CharacterAnimationView animationView)
        {
            _scoreEntity = scoreEntity;
            _turnPlayerEntity = turnPlayerEntity;
            _phaseEntity = phaseEntity;
            _animationView = animationView;
        }

        public void Initialize()
        {
            _scoreEntity.OnDanceAsObservable()
                .Subscribe(OnDance)
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
                case DancePhase.Audience:
                case DancePhase.TurnChange:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}