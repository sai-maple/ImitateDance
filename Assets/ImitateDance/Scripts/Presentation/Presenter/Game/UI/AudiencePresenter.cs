using System;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Presentation.View.Game;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class AudiencePresenter : IInitializable, IDisposable
    {
        private readonly PointEntity _pointEntity = default;
        private readonly SpeedEntity _speedEntity = default;
        private readonly DifficultyEntity _difficultyEntity = default;
        private readonly MusicEntity _musicEntity = default;
        private readonly AudienceAreaView _audienceView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public AudiencePresenter(PointEntity pointEntity, SpeedEntity speedEntity, DifficultyEntity difficultyEntity,
            MusicEntity musicEntity, AudienceAreaView audienceView)
        {
            _pointEntity = pointEntity;
            _speedEntity = speedEntity;
            _difficultyEntity = difficultyEntity;
            _musicEntity = musicEntity;
            _audienceView = audienceView;
        }

        public void Initialize()
        {
            var threshold = _difficultyEntity.Value switch
            {
                MusicDifficulty.Easy => 15,
                MusicDifficulty.Normal => 35,
                MusicDifficulty.Hard => 50,
                _ => throw new ArgumentOutOfRangeException()
            };
            _audienceView.Initialize(threshold);

            _pointEntity.OnSelfPointChangeAsObservable()
                .Subscribe(self => _audienceView.ChangePointSelf(self, _pointEntity.OpponentPoint))
                .AddTo(_disposable);

            _pointEntity.OnOpponentPointChangeAsObservable()
                .Subscribe(opponent => _audienceView.ChangePointOpponent(_pointEntity.SelfPoint, opponent))
                .AddTo(_disposable);

            _pointEntity.OnWinnerAsObservable()
                .Subscribe(_audienceView.Result)
                .AddTo(_disposable);

            _musicEntity.OnFinishAsObservable()
                .Subscribe(_ => _audienceView.Finish())
                .AddTo(_disposable);

            _speedEntity.OnChangeAsObservable()
                .Subscribe(_audienceView.SetSpeed)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}