using System;
using ImitateDunce.Applications.Data;
using ImitateDunce.Applications.Enums;
using ImitateDunce.Domain.Entity.Game.Core;
using ImitateDunce.Presentation.View.Game;
using UniRx;
using VContainer.Unity;

namespace ImitateDunce.Presentation.Presenter.Game.Character
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
            _scoreEntity.OnDunceAsObservable()
                .Subscribe(OnDunce)
                .AddTo(_disposable);
        }

        private void OnDunce(DunceData dunceData)
        {
            switch (_phaseEntity.Current)
            {
                case DuncePhase.Dunce:
                    _animationView.Dunce(dunceData.Dunce, _turnPlayerEntity.Current);
                    break;
                case DuncePhase.Demo:
                    _animationView.Dunce(dunceData.Demo, _turnPlayerEntity.Current);
                    break;
                case DuncePhase.Audience:
                case DuncePhase.TurnChange:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}